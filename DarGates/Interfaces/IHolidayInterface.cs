using DarGates.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IHolidayInterface
    {
        Task<ApiResponse<int>> AddOfficialHoliday(OfficialHolidayDTO officialHlidayDTO);
        Task<ApiResponse<bool>> DeleteOfficialHoliday(int iD,string UserId);
        ApiResponse<List<OfficialHolidayDTO>> GetOfficialHoliday();
        Task<ApiResponse<bool>> UpdateOfficialHoliday(OfficialHolidayDTO officialHolidayDTO);
        ApiResponse<List<WeeklyHolidayDTO>> GetWeeklyHoliday();
        Task<ApiResponse<bool>> UpdateWeeklyHoliday(WeeklyHolidayDTO weeklyHolidayDTO);
    }
}
