using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IHoursInterface
    {
        Task<ApiResponse<object>> CheckInHour(string UserId, IFormFile image1, IFormFile image2);
        Task<ApiResponse<bool>> ConfirmCheckOutHour(int id, string userId);
    }
}
