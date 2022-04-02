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
    public class InvitationRepository : IInvitationInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        private readonly UserManager<GardUser> _userManager;
        public InvitationRepository(DBContext context, IBasicServices services, IMapper mapper, UserManager<GardUser> userManager)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
            _userManager = userManager;
        }
        #region Invitation
        public async Task<ApiResponse<object>> AddInvitation(InvitationDTO.AddInvitation invitationDTO)
        {
            var response = new ApiResponse<object>();
            var date = Convert.ToDateTime(invitationDTO.Date).Date;
            var invitation = _mapper.Map<Invitation>(invitationDTO);
            invitation.CreationDate = date;
            await _context.AddAsync(invitation);
            await _context.SaveChangesAsync();
            invitation.QrCode = _services.EncryptQRCode(_services.GenerateRandomCode() + invitation.ID);
            _context.Update(invitation);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = new
            {
                Id = invitation.ID,
                qrCode = invitation.QrCode
            };
            return response;
        }
        public async Task<ApiResponse<object>> ScanInvitationQr(string QrCode)
        {
            var response = new ApiResponse<object>();
            string Qr = _services.DecryptQRCode(QrCode);
            int ID = int.Parse(Qr[8..]);
            var invitation = await _context.Invitation.FindAsync(ID);
            if (invitation == null || invitation.QrCode != QrCode || invitation.GateLogId != null)
            {
                response.Status = false;
                response.Message = "Invalid QR";
                return response;
            }
            bool IsVIP = false;
            if(invitation.InvitationTypeID == 1)
                IsVIP = true;
            response.Status = true;
            response.Message = "Success";
            response.Response = new
            {
                id = ID,
                isVIP = IsVIP
            };
            return response;
        }
        public async Task<ApiResponse<QrDTO>> CheckInInvitation(CheckInInvitationDTO checkInInvitationDTO, IFormFile image1, IFormFile image2)
        {
            var response = new ApiResponse<QrDTO>();
            try
            {
                var user = _userManager.FindByIdAsync(checkInInvitationDTO.UserID).Result;
                var invitation = _context.Invitation.Include(i => i.InvitationType).Where(i => i.ID == checkInInvitationDTO.InvitationId && i.GateLogId == null).SingleOrDefault();
                if (user == null || invitation == null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                var log = new GateLog
                {
                    ParkType = invitation.InvitationType.Type,
                    UserId = user.Id,
                    InDate = DateTime.Today,
                    InTime = DateTime.Now.ToString("hh: mm tt")
                };
                await _context.AddAsync(log);
                await _context.SaveChangesAsync();
                if (invitation.InvitationType.ID != 1)
                {
                    log.Image1 = await _services.UploadPhoto(image1, "Invitation", log.ID.ToString(), "CarID");
                    log.Image2 = await _services.UploadPhoto(image2, "Invitation", log.ID.ToString(), "PersonID");
                }
                log.QRCode = _services.EncryptQRCode(_services.GenerateRandomCode() + log.ID.ToString());
                invitation.GateLogId = log.ID;
                _context.Update(log);
                _context.Update(invitation);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = new QrDTO
                {
                    ID = log.ID,
                    PrintTime = DateTime.Now.ToString("dd-MM-yyyy") + " / " + DateTime.Now.ToShortTimeString(),
                    QrCode = log.QRCode
                };
            }
            catch (Exception ex)
            {

            }
            
            return response;
        }
        public ApiResponse<object> GetInvitations(string UserId, string From, string To)
        {
            var response = new ApiResponse<object>();
            DateTime start = DateTime.Today;
            DateTime end = DateTime.Today;
            if (From != null)
            {
                start = Convert.ToDateTime(From).Date;
                end = start;
            }
            if(To != null)
                end  = Convert.ToDateTime(To).Date;
            if (end < start)
                end = start;
            var invitations = _context.Invitation.Include(i => i.InvitationType).Include(i => i.GateLog)
                .Where(x => x.GardUserId == UserId
                && x.CreationDate >= start
                && x.CreationDate <= end)
                .Select(_mapper.Map<GetInvitationDTO>)
                .ToList();
            if (invitations == null)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            var types = GetInvitationTypes().Response;
            response.Status = true;
            response.Message = "Success";
            response.Response = new
            {
                Invitations = invitations,
                Types = types
            };
            return response;
        }
        public ApiResponse<List<InvitationDTO.InvitationType>> GetInvitationTypes()
        {
            var response = new ApiResponse<List<InvitationDTO.InvitationType>>();
            var types = _context.InvitationType.Select(_mapper.Map<InvitationDTO.InvitationType>).ToList();
            if (types == null)
            {
                response.Status=true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = types;
            return response;
        }
        public async Task<ApiResponse<bool>> DeleteInvitation(int id)
        {
            var response =new ApiResponse<bool>();
            
            var invitation = await _context.Invitation.FindAsync(id);
            if (invitation == null)
            {
                response.Status = false;
                response.Message = "Invalid Id";
                return response;
            }
            _context.Remove(invitation);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "Success";
            response.Response = true;
            return response;
        }
        #endregion
    }
}
