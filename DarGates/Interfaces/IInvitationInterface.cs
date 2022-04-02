using DarGates.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IInvitationInterface
    {
        Task<ApiResponse<object>> AddInvitation(InvitationDTO.AddInvitation invitationDTO);
        Task<ApiResponse<object>> ScanInvitationQr(string qrCode);
        Task<ApiResponse<QrDTO>> CheckInInvitation(CheckInInvitationDTO checkInInvitationDTO, IFormFile image1, IFormFile image2);
        ApiResponse<object> GetInvitations(string UserId, string From, string To);
        ApiResponse<List<InvitationDTO.InvitationType>> GetInvitationTypes();
        Task<ApiResponse<bool>> DeleteInvitation(int id);
    }
}
