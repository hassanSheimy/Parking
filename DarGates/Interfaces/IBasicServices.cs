using DarGates.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IBasicServices
    {
        public Task<JwtSecurityToken> GenerateJwt(GardUser user);
        bool IsTodayHoliday();
        Task<string> UploadPhoto(IFormFile file, string folder, string id, string n);
        public string GenerateRandomCode();
        public string DecryptQRCode(string QR);
        public string EncryptQRCode(string randomCode);
        Task SaveErrorLog(string MsgErr, string EndP);
        Task SaveLogIn(string Id,IFormFile image);
        //Task SaveLogOut(string Id);
    }
}
