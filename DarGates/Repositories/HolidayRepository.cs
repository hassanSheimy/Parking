using AutoMapper;
using DarGates.DB;
using DarGates.DTOs;
using DarGates.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Repositories
{
    public class HolidayRepository : IHolidayInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        public HolidayRepository(DBContext context, IBasicServices services, IMapper mapper)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
        }
        #region OfficialHoliday
        public async Task<ApiResponse<int>> AddOfficialHoliday(OfficialHolidayDTO officialHlidayDTO)
        {
            var response = new ApiResponse<int>();
            try
            {
                var holiday = new OfficialHoliday
                {
                    StartDate = Convert.ToDateTime(officialHlidayDTO.StartDate).Date,
                    EndDate = Convert.ToDateTime(officialHlidayDTO.EndDate).Date,
                    Description = officialHlidayDTO.Description
                };
                await _context.OfficialHoliday.AddAsync(holiday);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = holiday.Id;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "AddOfficialHoliday");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> DeleteOfficialHoliday(int ID,string UserId)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var holiday = await _context.OfficialHoliday.FindAsync(ID);
                if (holiday == null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                _context.Remove(holiday);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "DeleteOfficialHoliday");
            }
            return response;
        }
        public ApiResponse<List<OfficialHolidayDTO>> GetOfficialHoliday()
        {
            var response = new ApiResponse<List<OfficialHolidayDTO>>();
            var holidays = _context.OfficialHoliday.Select(_mapper.Map<OfficialHolidayDTO>).ToList();
            if (holidays == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = holidays;
            return response;
        }
        public async Task<ApiResponse<bool>> UpdateOfficialHoliday(OfficialHolidayDTO officialHlidayDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var holiday = await _context.OfficialHoliday.FindAsync(officialHlidayDTO.Id);
                if (holiday == null || Convert.ToDateTime(officialHlidayDTO.StartDate) > Convert.ToDateTime(officialHlidayDTO.EndDate)) 
                {
                    response.Status = false;
                    response.Message = "null";
                    return response;
                }
                holiday.StartDate = Convert.ToDateTime(officialHlidayDTO.StartDate);
                holiday.EndDate = Convert.ToDateTime(officialHlidayDTO .EndDate);
                holiday.Description = officialHlidayDTO.Description;
                _context.Update(holiday);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "AddOfficialHoliday");
            }
            return response;
        }
        #endregion

        #region WeeklyHoliday
        public ApiResponse<List<WeeklyHolidayDTO>> GetWeeklyHoliday()
        {
            var response = new ApiResponse<List<WeeklyHolidayDTO>>();
            var holidays = _context.WeeklyHoliday.Select(_mapper.Map<WeeklyHolidayDTO>).ToList();
            if (holidays == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = holidays;
            return response;
        }
        public async Task<ApiResponse<bool>> UpdateWeeklyHoliday(WeeklyHolidayDTO weeklyHolidayDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var holiday = await _context.WeeklyHoliday.FindAsync(weeklyHolidayDTO.Id);
                if (holiday == null) 
                {
                    response.Status = false;
                    response.Message = "null";
                    return response;
                }
                holiday.IsHoliday = weeklyHolidayDTO.IsHoliday;
                _context.Update(holiday);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "AddOfficialHoliday");
            }
            return response;
        }
        #endregion
    }
}
