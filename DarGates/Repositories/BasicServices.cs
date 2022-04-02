using DarGates.DB;
using DarGates.Helpers;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DarGates.Repositories
{
    public class BasicServices : IBasicServices
    {
        private readonly IWebHostEnvironment _environment;
        private readonly DBContext _context;
        private readonly UserManager<GardUser> _userManager;
        private readonly JWT _jwt;
        public BasicServices(IWebHostEnvironment environment, DBContext context, UserManager<GardUser> userManager,IOptions<JWT> jwt)
        {
            _environment = environment;
            _context = context;
            _userManager = userManager;
            _jwt = jwt.Value;
        }
        public async Task<JwtSecurityToken> GenerateJwt(GardUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", user.Id)
            }.Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMonths(1),
                    signingCredentials: signInCredentials
                );
            return jwtSecurityToken;
        }
        public bool IsTodayHoliday()
        {
            int number = ((int)DateTime.Now.DayOfWeek);
            var week = _context.WeeklyHoliday.Where(t => t.Number == number).SingleOrDefault();
            if (week.IsHoliday)
            {
                return true;
            }

            var official = _context.OfficialHoliday.Where(t => t.StartDate <= DateTime.Today && t.EndDate >= DateTime.Today).FirstOrDefault();
            if (official != null)
            {
                return true;
            }
            return false;
        }

        public async Task<string> UploadPhoto(IFormFile file, string folder ,string id, string n)
        {
            try
            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Images\\" + folder))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Images\\" + folder);
                    }
                    string name = DateTime.Today.ToString("dd-MM-yyyy") + "_" + id + "_" + n + Path.GetExtension(file.FileName);
                    await using FileStream fileStream = File.Create(_environment.WebRootPath + "\\Images\\"+folder+"\\" + name);
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return "\\Images\\"+ folder + "\\" + name;
                }
                else
                {
                    return "Failure";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public string GenerateRandomCode()
        {
            var chars = "Any Long Key";

            var stringChars = new char[8];

            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
        public string DecryptQRCode(string qrCode)
        {
            char[] result = new char[50];
            string ENC = "Any Long Key";
            int i = 0;
            for (; i < qrCode.Length; i++)
            {
                result[i] = (char)(qrCode[i] ^ ENC[i]);
            }
            string s = new string(result)[0..^(result.Length - i)];
            return s;
        }
        public string EncryptQRCode(string randomCode)
        {
            char[] result = new char[50];
            for (int j = 0; j < 50; j++)
            {
                result[j] = 'X';
            }
            string ENC = "Any Long Key";
            int i = 0;
            for (; i < randomCode.Length; i++)
            {
                result[i]= (char)(randomCode[i] ^ ENC[i]);
            }
            string s = new string(result)[0..^(result.Length - i)];
            return s;
        }
        public async Task SaveErrorLog(string MsgErr,string EndP)
        {
            var err = new GardErrorLog { Message = MsgErr, EndPoint = EndP, DateTime = DateTime.Now };
            await _context.GardErrorLog.AddAsync(err);
            await _context.SaveChangesAsync();
        }
        public async Task SaveLogIn(string Id,IFormFile image)
        {
            var logIn = new SignInLog
            {
                GardUserId = Id,
                LogInDate = DateTime.Today,
                LogInTime = DateTime.Now.ToString("hh: mm tt"),
                
            };
            await _context.SignInLog.AddAsync(logIn);
            await _context.SaveChangesAsync();
            logIn.Image = await UploadPhoto(image, "SignIn", logIn.Id.ToString(), "UserSignIn");
            _context.Update(logIn);
            await _context.SaveChangesAsync();
        }
    }
}
