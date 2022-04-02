using DarGates.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IUserInterface
    {
        Task<ApiResponse<bool>> RegisterGuardUser(UserDTO.Request userDTO);
        Task<ApiResponse<bool>> UpdateUser(DarUserDTO userDTO);
        Task<ApiResponse<object>> LogIn(SignIn LogInDTO, IFormFile image);
        Task<ApiResponse<bool>> RegisterAdmin(UserDTO.Request userDTO);
        ApiResponse<List<DarUserDTO>> GetUsers();
        Task<ApiResponse<bool>> RegisterManager(UserDTO.Request userDTO);
        Task<ApiResponse<bool>> LogOut(string userId);
        Task<ApiResponse<bool>> LogOutAllDevices(string userId);
        ApiResponse<List<DarUserDTO>> GetOnlineUsers();
    }
}
