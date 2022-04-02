using DarGates.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IGateInterface
    {
        #region User
        Task<ApiResponse<BillDTO>> Bill(BillDTO billDTO);
        Task<ApiResponse<CheckInDTO.Response>> CheckIn(CheckInDTO.Request CheckInDTO, IFormFile file, IFormFile file1);
        Task<ApiResponse<object>> CheckOut(string qrCode);
        Task<ApiResponse<bool>> Cancel(int LogId, string UserId);
        #endregion
        ApiResponse<ParkedReasonDTO> Parked(string parkType, int pageNo);
        Task<ApiResponse<object>> Print(string UserID, int LogID, int ReasonID);
        Task<ApiResponse<bool>> UnPrint(int Id);
        ApiResponse<GateLogDTO> GetLogByID(int ID);
        ApiResponse<LogSummaryDTO> GetGateLogs(int? Id, string ParkType, string Start, string End);
        Task<ApiResponse<object>> AddGate(GateDTO gateDTO);
        ApiResponse<LogSummaryDTO> FilterLogs(string StartDate, string EndDate, string UserId, string ParkType,int PageNo);
        ApiResponse<LogSummaryDTO> ReportLogs(DateTime StartDate, DateTime EndDate, string UserId, string ParkType, int PageNo);
        ApiResponse<LogSummaryDTO> GetUnPayedBills(string UserID);
        Task<ApiResponse<bool>> PayInvoices(PayInvoiceDTO payInvoiceDTO);
        Task<ApiResponse<bool>> AddReason(ReasonDTO reasonDTO);
        ApiResponse<List<GateDTO>> GetAllGates();
        ApiResponse<List<ReasonDTO>> GetReasons();
        ApiResponse<List<PrinterLogDTO>> GetPrinterLog(string UserID);
        Task<ApiResponse<bool>> ClearTheBalance(string userID);
        ApiResponse<object> ParkedForAdmin();
        ApiResponse<object> SummaryForToday();
    }
}
