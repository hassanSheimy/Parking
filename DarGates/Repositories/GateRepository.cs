using AutoMapper;
using DarGates.DB;
using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Repositories
{
    public class GateRepository : IGateInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        private readonly UserManager<GardUser> _userManager;
        public GateRepository(DBContext context, IBasicServices services, IMapper mapper, UserManager<GardUser> userManager)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
            _userManager = userManager;
        }

        #region GateLogs
        public async Task<ApiResponse<object>> AddGate(GateDTO gateDTO)
        {
            var response = new ApiResponse<object>();
            var gate = _mapper.Map<Gate>(gateDTO);
            await _context.AddAsync(gate);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = new { gate.Id };
            return response;
        }
        public ApiResponse<List<GateDTO>> GetAllGates()
        {
            var response = new ApiResponse<List<GateDTO>>();
            var gates = _context.Gate.Select(_mapper.Map<GateDTO>).ToList();
            response.Status = true;
            response.Message = "Success";
            response.Response = gates;
            return response;
        }
        public async Task<ApiResponse<BillDTO>> Bill(BillDTO billDTO)
        {
            var response = new ApiResponse<BillDTO>();
            try
            {
                bool IsHoliday = _services.IsTodayHoliday();
                var Car = await _context.Owner.FindAsync(billDTO.TypeID);
                var Militry = await _context.Owner.FindAsync(1);
                var Civil = await _context.Owner.FindAsync(2);

                billDTO.CivilPrice = (IsHoliday ? Civil.PriceInHoliday : Civil.Price) * billDTO.CivilCount;
                billDTO.MilitryPrice = (IsHoliday ? Militry.PriceInHoliday : Militry.Price) * billDTO.MilitryCount;
                billDTO.Price = IsHoliday ? Car.PriceInHoliday : Car.Price;
                billDTO.Total = billDTO.Price + billDTO.CivilPrice + billDTO.MilitryPrice;

                response.Status = true;
                response.Message = "Success";
                response.Response = billDTO;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.Message, "Bill");
            }
            return response;
        }
        public async Task<ApiResponse<CheckInDTO.Response>> CheckIn(CheckInDTO.Request logDTO, IFormFile file, IFormFile file1)
        {
            var response = new ApiResponse<CheckInDTO.Response>();
            try
            {
                bool IsHoliday = _services.IsTodayHoliday();
                var Car = await _context.Owner.FindAsync(logDTO.TypeID);
                var Militry = await _context.Owner.FindAsync(1);
                var Civil = await _context.Owner.FindAsync(2);

                var gateLog = new GateLog
                {
                    ParkType = Car.Type,
                    Price = IsHoliday ? Car.PriceInHoliday : Car.Price,
                    MilitryCount = logDTO.MilitryCount,
                    CivilCount = logDTO.CivilCount,
                    CivilPrice = IsHoliday ? Civil.PriceInHoliday : Civil.Price,
                    MilitryPrice = IsHoliday ? Militry.PriceInHoliday : Militry.Price,
                    UserId = logDTO.UserID,
                    IsPayed = false,
                    Total = IsHoliday ? Car.PriceInHoliday + (Militry.PriceInHoliday * logDTO.MilitryCount) + (Civil.PriceInHoliday * logDTO.CivilCount)
                                    : Car.Price + (Militry.Price * logDTO.MilitryCount) + (Civil.Price * logDTO.CivilCount),
                    InDate = DateTime.Today,
                    InTime = DateTime.Now.ToString("hh: mm tt"),
                };
                await _context.GateLog.AddAsync(gateLog);
                await _context.SaveChangesAsync();
                logDTO.ID = gateLog.ID;

                gateLog.QRCode = _services.EncryptQRCode(_services.GenerateRandomCode() + gateLog.ID.ToString());
                gateLog.Image1 = await _services.UploadPhoto(file, "GateLog", gateLog.ID.ToString(), "CarID");
                gateLog.Image2 = await _services.UploadPhoto(file1, "GateLog", gateLog.ID.ToString(), "PersonID");
                _context.GateLog.Update(gateLog);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = new CheckInDTO.Response
                {
                    ID = gateLog.ID,
                    CivilPrice = gateLog.CivilPrice * logDTO.CivilCount,
                    MilitryPrice = gateLog.MilitryPrice * logDTO.MilitryCount,
                    Price = gateLog.Price,
                    Total = gateLog.Total,
                    QrCode = gateLog.QRCode,
                    PrintTime = DateTime.Now.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("hh: mm tt")
                };
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "CheckIn");
            }
            return response;
        }
        public double CalculateHours(DateTime In)
        {
            bool IsHoliday = _services.IsTodayHoliday();
            var Type = _context.Owner.Where(o => o.Type == "المحاسبه بالساعه").SingleOrDefault();
            var TotalMinutes = ((int)(DateTime.Now - In).TotalMinutes);
            var Hours = (IsHoliday ? Type.PriceInHoliday : Type.Price) * (TotalMinutes / 60);
            double Minutes = TotalMinutes % 60 > 45 ? 60 : (TotalMinutes % 60 > 15 ? 30 : 0);
            return Hours + (Minutes / 60 * (IsHoliday ? Type.PriceInHoliday : Type.Price));
        }
        public async Task<ApiResponse<object>> CheckOut(string QrCode)
        {
            var response = new ApiResponse<object>();
            try
            {
                int id = int.Parse(_services.DecryptQRCode(QrCode)[8..]);
                var gatelog = _context.GateLog.Where(g => g.ID == id && !g.IsDeleted).SingleOrDefault();
                if (gatelog == null || gatelog.OutDate != null || gatelog.QRCode != QrCode)
                {
                    response.Status = false;
                    response.Message = "كود غير صحيح";
                    if(gatelog.OutDate != null)
                        response.Message = "تم الخروج من قبل !";
                    return response;
                }
                if (gatelog.ParkType == "المحاسبه بالساعه" && gatelog.OutDate == null)
                {
                    var Total = CalculateHours(Convert.ToDateTime(gatelog.InDate.Value.Date.ToShortDateString() + " " +gatelog.InTime));
                    gatelog.Price = ((float)Total);
                    response.Status = true;
                    response.Message = "Success";
                    response.Response = new { 
                        IsPayed = false,
                        Id = id,
                        QrCode = QrCode,
                        Total = Total,
                        InTime = gatelog.InDate.Value.ToString("dd-MM-yyyy")+ " " + gatelog.InTime,
                        OutTime = DateTime.Now.ToString("dd-MM-yyyy") + " " + DateTime.Now.ToString("hh: mm tt")
                    };
                }
                else
                {
                    gatelog.OutDate = DateTime.Today;
                    gatelog.OutTime = DateTime.Now.ToString("hh: mm tt");
                    response.Response = true;
                }
                _context.GateLog.Update(gatelog);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                //_context.ChangeTracker.Clear();
                await _services.SaveErrorLog(ex.ToString(), "CheckOut");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> Cancel(int LogId, string UserId)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var gateLog = _context.GateLog.Include(g => g.PrinterLogs).Where(i => i.ID == LogId).SingleOrDefault();
                var user = await _userManager.FindByIdAsync(UserId);
                if (gateLog == null || user == null)
                {
                    response.Status = false;
                    response.Message = "Incorrect LogId";
                    return response;
                }
                gateLog.IsDeleted = true;
                gateLog.DeletedByUserId = UserId;
                gateLog.DeleteTime = DateTime.Now;
                foreach (var print in gateLog.PrinterLogs)
                {
                    if (!print.IsDeleted)
                    {
                        print.IsDeleted = true;
                        print.DeleteTime = gateLog.DeleteTime;
                        print.DeletedByUserId = UserId;
                    }
                }
                _context.Update(gateLog);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "Cancel");
            }
            return response;
        }
        public ApiResponse<ParkedReasonDTO> Parked(string parkType, int pageNo)
        {
            var response = new ApiResponse<ParkedReasonDTO>();
            try
            {
                var log = _context.GateLog.Where(l => l.OutDate == null
                    && !l.IsDeleted
                    && l.ParkType == (parkType ??l.ParkType)).OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<ParkedDTO>).ToList();
                if (log == null)
                {
                    response.Status = true;
                    response.Message = "null";
                    return response;
                }

                response.Status = true;
                response.Message = "Success";
                response.Response = new ParkedReasonDTO
                {
                    ParkedDTO = log.Skip((pageNo - 1) * 5).Take(5).ToList(),
                    ReasonDTO = GetReasons().Response,
                    Count = log.Count
                };
            }
            catch (Exception ex)
            {
                _services.SaveErrorLog(ex.InnerException.ToString(), "Parked");
            }
            return response;
        }
        public ApiResponse<object> ParkedForAdmin()
        {
            var response = new ApiResponse<object>();
            try
            {
                //var data = new List<ParkedReasonDTO>();
                var log = _context.GateLog.Where(l => l.OutDate == null && !l.IsDeleted).OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
                if (log.Count == 0)
                {
                    response.Status = true;
                    response.Message = "No Parked";
                    return response;
                }
                response.Status = true;
                response.Message = "Success";
                var data = new
                {
                    SummaryDTO = CalcSummary(ref log),
                    ParkedDTO = log
                };
                response.Response = data;
            }
            catch (Exception ex)
            {
                _services.SaveErrorLog(ex.InnerException.ToString(), "Parked");
            }
            return response;
        }
        public ApiResponse<object> SummaryForToday()
        {
            var response = new ApiResponse<object>();
            try
            {
                var log = _context.GateLog.Include(l => l.PrinterLogs)
                .Where(log => log.InDate == DateTime.Now.Date && !log.IsDeleted)
                .OrderByDescending(i => i.ID)
                .Select(_mapper.Map<GateLogDTO>).ToList();
                var types = _context.Owner.Skip(2).Where(o => !o.IsDeleted).Select(_mapper.Map<ParkTypeCountDTO>).ToList();
                types.AddRange(_context.InvitationType.Select(_mapper.Map<ParkTypeCountDTO>));
                response.Status = true;
                response.Message = "Success";
                response.Response = new
                {
                    SummaryDTO = CalcSummary(ref log),
                    ParkTypeCountDTO = CountParkType(ref log, ref types)
                };
            }
            catch (Exception ex)
            {
                _services.SaveErrorLog(ex.InnerException.ToString(), "Parked");
            }
            return response;
        }
        public ApiResponse<GateLogDTO> GetLogByID(int ID)
        {
            var response = new ApiResponse<GateLogDTO>();
            try
            {
                var log = _context.GateLog.Include(l => l.PrinterLogs).Where(l => l.ID == ID).Select(_mapper.Map<GateLogDTO>).SingleOrDefault();
                if(log == null)
                {
                    response.Status = false;
                    response.Message = "null";
                    return response;
                }
                log.CivilPrice = log.Price * log.CivilCount;
                log.MilitryPrice = log.MilitryPrice * log.MilitryCount;
                response.Status = true;
                response.Message = "Success";
                response.Response = log;
            }
            catch (Exception ex)
            {
                _services.SaveErrorLog(ex.InnerException.ToString(), "GetLogByID");
            }
            return response;
        }
        public ApiResponse<LogSummaryDTO> GetGateLogs(int? Id, string ParkType, string In, string Out)
        {
            var response = new ApiResponse<LogSummaryDTO>();
            var log = new List<GateLogDTO>();
            if (Id != null)
            {
                log = _context.GateLog.Include(l => l.PrinterLogs).Where(log => log.ID == Id && !log.IsDeleted)
                    .OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
            }
            else if (ParkType != null) 
            {
                log = _context.GateLog.Include(l => l.PrinterLogs).Where(log => log.ParkType.Contains(ParkType) && !log.IsDeleted)
                    .OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
            }
            else if(In != null)
            {
                var InDate = Convert.ToDateTime(In);
                log = _context.GateLog.Include(l => l.PrinterLogs).Where(log => log.InDate == InDate && !log.IsDeleted)
                    .OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
            }
            else if (Out != null)
            {
                var OutDate = Convert.ToDateTime(Out);
                log = _context.GateLog.Include(l => l.PrinterLogs).Where(log => log.OutDate == OutDate && !log.IsDeleted)
                    .OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
            }
            else
            {
                log = _context.GateLog.Include(l => l.PrinterLogs).Where(log => !log.IsDeleted)
                    .OrderByDescending(i => i.ID)
                    .Select(_mapper.Map<GateLogDTO>).ToList();
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = new LogSummaryDTO
            {
                LogDTO = log,
                
                SummaryDTO = CalcSummary(ref log)
            };
            return response;
        }
        public ApiResponse<LogSummaryDTO> FilterLogs(string StartDate, string EndDate, string UserId, string ParkType,int PageNo)
        {
            var response = new ApiResponse<LogSummaryDTO>();
            var log = _context.GateLog.Include(l => l.PrinterLogs)
                .Where(log => log.InDate >= (StartDate != null ? Convert.ToDateTime(StartDate) : log.InDate)
                    && log.InDate <= (EndDate != null ? Convert.ToDateTime(EndDate) : log.InDate)
                    && !log.IsDeleted
                    && log.UserId == (UserId ?? log.UserId)
                    && log.ParkType == (ParkType ?? log.ParkType))
                .OrderByDescending(i => i.ID)
                .Select(_mapper.Map<GateLogDTO>).ToList();
            var types = _context.Owner.Skip(2).Where(o => !o.IsDeleted).Select(_mapper.Map<ParkTypeCountDTO>).ToList();
            types.AddRange(_context.InvitationType.Select(_mapper.Map<ParkTypeCountDTO>));
            response.Status = true;
            response.Message = "Success";
            response.Response = new LogSummaryDTO
            {
                LogDTO = log.Skip((PageNo-1)*20).Take(20).ToList(),
                SummaryDTO = CalcSummary(ref log),
                ParkTypeCountDTO = CountParkType(ref log ,ref types)
            };
            return response;
        }
        public ApiResponse<LogSummaryDTO> ReportLogs(DateTime StartDate, DateTime EndDate, string UserId, string ParkType,int PageNo)
        {
            var response = new ApiResponse<LogSummaryDTO>();
            var log = _context.GateLog.Include(l => l.PrinterLogs)
                .Where(log => log.InDate >= StartDate
                    && log.InDate <= EndDate
                    && !log.IsDeleted
                    && log.UserId == (UserId ?? log.UserId)
                    && log.ParkType == (ParkType ?? log.ParkType))
                .OrderByDescending(i => i.ID)
                .Select(_mapper.Map<GateLogDTO>).ToList();
            var types = _context.Owner.Skip(2).Where(o => !o.IsDeleted).Select(_mapper.Map<ParkTypeCountDTO>).ToList();
            types.AddRange(_context.InvitationType.Select(_mapper.Map<ParkTypeCountDTO>));
            response.Status = true;
            response.Message = "Success";
            response.Response = new LogSummaryDTO
            {
                SummaryDTO = CalcSummary(ref log),
                ParkTypeCountDTO = CountParkType(ref log, ref types),
                LogDTO = log.Skip((PageNo - 1) * 10).Take(10).ToList(),
            };
            return response;
        }
        public async Task<ApiResponse<bool>> UnPrint(int Id)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var log = await _context.GateLog.FindAsync(Id);
                if (log == null || log.OutTime != null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                log.IsPayed = false;
                _context.GateLog.UpdateRange(log);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "UnPrint");
            }
            return response;
        }
        #endregion

        #region Printer
        public async Task<ApiResponse<object>> Print(string UserID, int LogID, int ReasonID)
        {
            var response = new ApiResponse<object>();
            try
            {
                var log = await _context.GateLog.FindAsync(LogID);
                var reason = await _context.RePrintReason.FindAsync(ReasonID);
                var user = await _userManager.FindByIdAsync(UserID);
                if (log == null || reason == null || user == null || log.OutDate != null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                var printerLog = new PrinterLog
                {
                    UserID = user.Id,
                    GateLogID = log.ID,
                    RePrintReasonID = reason.ID,
                    Price = (log.ParkType.Contains("VIP") || log.ParkType.Contains("Normal")) ? 0 : reason.Price,
                    PrintDate = DateTime.Today,
                    PrintTime = DateTime.Now.ToString("hh: mm tt")
                };
                await _context.PrinterLog.AddAsync(printerLog);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = new { PrintTime = DateTime.Now.ToString("dd-MM-yyyy") + " / " + DateTime.Now.ToShortTimeString() };
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "Print");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> AddReason(ReasonDTO reasonDTO)
        {
            var response = new ApiResponse<bool>();
            var reason = new RePrintReason { Reason = reasonDTO.Reason, Price = reasonDTO.Price };
            await _context.RePrintReason.AddAsync(reason);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = true;
            return response;
        }
        public ApiResponse<List<ReasonDTO>> GetReasons()
        {
            var response = new ApiResponse<List<ReasonDTO>>();
            var reasons = _context.RePrintReason.Select(_mapper.Map<ReasonDTO>).ToList();
            if (reasons == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = reasons;
            return response;
        }
        public ApiResponse<List<PrinterLogDTO>> GetPrinterLog(string UserID)
        {
            var response = new ApiResponse<List<PrinterLogDTO>>();
            var reasons = _context.PrinterLog.Include(p => p.RePrintReason).Include(p => p.User).Where(p => p.UserID == (UserID ?? p.UserID))
                .OrderByDescending(i => i.ID)
                .Select(_mapper.Map<PrinterLogDTO>).ToList();
            if (reasons == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = reasons;
            return response;
        }
        #endregion

        #region Financial Managment
        public static SummaryDTO CalcSummary(ref List<GateLogDTO> logDTOs)
        {
            var summary = new SummaryDTO
            {
                Count = logDTOs.Count,
                ActivityCarCount = logDTOs.Where(l => l.ParkType == "انشطه").ToList().Count,
                CitizenCarCount = logDTOs.Where(c => c.ParkType == "مدنى").ToList().Count,
                MemberCarCount = logDTOs.Where(m=>m.ParkType== "عضو دار").ToList().Count,
                MilitryCarCount = logDTOs.Where(m=>m.ParkType== "ق.م").ToList().Count,
                CivilCount = logDTOs.Sum(m=> m.CivilCount),
                MilitryCount = logDTOs.Sum(c=> c.MilitryCount),
                ParkPrice = logDTOs.Sum(l => l.Price),
                CivilPrice = logDTOs.Sum(l => l.CivilPrice * l.CivilCount),
                MilitryPrice = logDTOs.Sum(l => l.MilitryPrice * l.MilitryCount),
                Total = logDTOs.Sum(l => l.Total),
                RePrintFines = logDTOs.Sum(l => l.RePrintFines)
            };
            summary.Total_Fines = summary.Total + summary.RePrintFines;
            return summary;
        }
        public static List<ParkTypeCountDTO> CountParkType(ref List<GateLogDTO> logDTOs,ref List<ParkTypeCountDTO> parkTypeCountDTO)
        {
            foreach(var parkType in parkTypeCountDTO)
            {
                parkType.Count = logDTOs.Where(l => l.ParkType == parkType.ParkType).ToList().Count;
            }
            return parkTypeCountDTO;
        }
        public ApiResponse<LogSummaryDTO> GetUnPayedBills(string UserID)
        {
            var response = new ApiResponse<LogSummaryDTO>();
            var logs = _context.GateLog.Where(l => l.UserId == UserID
                && l.IsPayed == false && !l.IsDeleted)
                .OrderByDescending(i => i.ID)
                .Select(_mapper.Map<GateLogDTO>).ToList();
            var fines = _context.PrinterLog.Where(l => l.UserID == UserID
                && l.IsPayed == false && l.DeleteTime == null)
                .OrderByDescending(i => i.ID).ToList();
            if (logs == null && fines == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = new LogSummaryDTO
            {
                LogDTO = logs,
                SummaryDTO = CalcSummary(ref logs)
            };
            response.Response.SummaryDTO.RePrintFines = fines.Sum(f=>f.Price);
            response.Response.SummaryDTO.Total_Fines = response.Response.SummaryDTO.RePrintFines + response.Response.SummaryDTO.Total;
            return response;
        }
        public async Task<ApiResponse<bool>> PayInvoices(PayInvoiceDTO payInvoiceDTO)
        {
            var response = new ApiResponse<bool>();
            var Invoices = _context.GateLog.Where(l => payInvoiceDTO.GateLogIDs.Contains(l.ID)).ToList();
            var fines = _context.PrinterLog.Where(l => payInvoiceDTO.PrinterLogIDs.Contains(l.ID)).ToList();
            if (Invoices.Count != payInvoiceDTO.GateLogIDs.Count || fines.Count != payInvoiceDTO.PrinterLogIDs.Count)
            {
                response.Status = false;
                response.Message = "null";
                return response;
            }
            foreach ( var invoice in Invoices)
            {
                if (invoice.IsPayed)
                {
                    response.Message = "false";
                    return response;
                }
                invoice.IsPayed = true;
            }
            foreach (var fine in fines)
            {
                if (fine.IsPayed)
                {
                    response.Message = "false";
                    return response;
                }
                fine.IsPayed = true;
            }
            _context.UpdateRange(Invoices);
            _context.UpdateRange(fines);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = true;
            return response;
        }
        public async Task<ApiResponse<bool>> ClearTheBalance(string UserID)
        {
            var response = new ApiResponse<bool>();
            var bills = _context.GateLog.Where(g => g.UserId == UserID && g.IsPayed == false && !g.IsDeleted).ToList();
            var printlog = _context.PrinterLog.Where(g => g.UserID == UserID && g.IsPayed == false && g.DeleteTime == null).ToList();
            if (bills == null && printlog == null)
            {
                response.Status = false;
                response.Message = "No Bills Found";
                return response;
            }
            if (bills != null)
            {
                foreach (var bill in bills)
                {
                    bill.IsPayed = true;
                }
                _context.UpdateRange(bills);
            }
            if (printlog != null)
            {
                foreach (var log in printlog)
                {
                    log.IsPayed = true;
                }
                _context.UpdateRange(printlog);
            }
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = true;
            return response;
        }
        #endregion
    }
}