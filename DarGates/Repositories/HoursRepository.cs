using AutoMapper;
using DarGates.DB;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Repositories
{
    public class HoursRepository : IHoursInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        private readonly UserManager<GardUser> _userManager;
        public HoursRepository(DBContext context, IBasicServices services, IMapper mapper, UserManager<GardUser> userManager)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<object>> CheckInHour(string UserId, IFormFile image1, IFormFile image2)
        {
            var response = new ApiResponse<object>();
            var user = await _userManager.FindByIdAsync(UserId);
            try
            {
                var role = _userManager.GetRolesAsync(user).Result.SingleOrDefault();
                if (role != "Guard")
                {
                    // Some Logic
                }
                var log = new GateLog
                {
                    ParkType = "المحاسبه بالساعه",
                    InDate = DateTime.Today,
                    InTime = DateTime.Now.ToString("hh: mm tt"),
                    UserId = UserId
                };
                await _context.AddAsync(log);
                await _context.SaveChangesAsync();
                log.QRCode = _services.EncryptQRCode(_services.GenerateRandomCode() + log.ID.ToString());
                log.Image1 = await _services.UploadPhoto(image1, "GateLog", log.ID.ToString(), "CarID");
                log.Image2 = await _services.UploadPhoto(image2, "GateLog", log.ID.ToString(), "PersonID");
                _context.GateLog.Update(log);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = new
                {
                    Id = log.ID,
                    QrCode = log.QRCode,
                    InTime = log.InDate.Value.ToString("dd-MM-yyyy") + " " + log.InTime
                };
            }
            catch (Exception ex)
            {
                _context.ChangeTracker.Clear();
                await _services.SaveErrorLog(ex.InnerException.ToString(), "CheckInHour");
            }
            return response;
        }

        public async Task<ApiResponse<bool>> ConfirmCheckOutHour(int id, string userId)
        {
            var response = new ApiResponse<bool>();
            var log = await _context.GateLog.FindAsync(id);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || log == null || log.ParkType != "المحاسبه بالساعه")
            {
                response.Message = "error";
                return response;
            }
            log.UserId = user.Id;
            log.Total = log.Price;
            log.OutDate = DateTime.Today;
            log.OutTime = DateTime.Now.ToString("hh: mm tt");

            _context.GateLog.Update(log);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = true;
            return response;
        }
    }
}
