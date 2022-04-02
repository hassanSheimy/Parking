using AutoMapper;
using DarGates.DB;
using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IGateInterface _gateInterface;
        private readonly IMapper _mapper;
        private readonly UserManager<GardUser> _userManager;
        private readonly SignInManager<GardUser> _signInManager;
        public UserRepository(DBContext context, IBasicServices services, IMapper mapper, IGateInterface gateInterface, UserManager<GardUser> userManager, SignInManager<GardUser> signInManager)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
            _gateInterface = gateInterface;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Authentication
        public async Task<ApiResponse<bool>> RegisterAdmin(UserDTO.Request userDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var user = new GardUser
                {
                    Name = userDTO.Name,
                    UserName = userDTO.UserName,
                    Rank = userDTO.Rank,                    
                };
                var create = await _userManager.CreateAsync(user, userDTO.Password);
                if (!create.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in create.Errors)
                    {
                        errors += $"{error.Description},";
                    }
                    response.Message = errors;
                    return response;
                }
                await _userManager.AddToRoleAsync(user, "Admin");

                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                //_context.GardUser.Remove(_context.GardUser.OrderBy(p=>p.ID).Last());
                await _services.SaveErrorLog(ex.InnerException.Message, "RegisterAdmin");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> RegisterManager(UserDTO.Request userDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var user = new GardUser
                {
                    Name = userDTO.Name,
                    UserName = userDTO.UserName,
                    Rank = userDTO.Rank,
                };
                var create = await _userManager.CreateAsync(user, userDTO.Password);
                if (!create.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in create.Errors)
                    {
                        errors += $"{error.Description},";
                    }
                    response.Message = errors;
                    return response;
                }
                await _userManager.AddToRoleAsync(user, "Manager");

                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                //_context.GardUser.Remove(_context.GardUser.OrderBy(p=>p.ID).Last());
                await _services.SaveErrorLog(ex.InnerException.Message, "RegisterAdmin");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> RegisterGuardUser(UserDTO.Request userDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var user = new GardUser
                {
                    Name = userDTO.Name,
                    UserName = userDTO.UserName,
                    Rank = userDTO.Rank,
                    GateID = userDTO.GateID
                };
                var create = await _userManager.CreateAsync(user, userDTO.Password);
                if (!create.Succeeded)
                {
                    var errors = string.Empty;
                    foreach (var error in create.Errors)
                    {
                        errors += $"{error.Description},";
                    }
                    response.Message = errors;
                    return response;
                }
                await _userManager.AddToRoleAsync(user, "Guard");

                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                //_context.GardUser.Remove(_context.GardUser.OrderBy(p=>p.ID).Last());
                await _services.SaveErrorLog(ex.InnerException.Message, "RegisterGuardUser");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> UpdateUser(DarUserDTO userDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var user = await _userManager.FindByIdAsync(userDTO.Id);
                if (user == null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                
                user.UserName = userDTO.UserName ?? user.UserName;
                //user.PasswordHash = userDTO.Password ?? user.PasswordHash;s
                user.Name = userDTO.Name ?? user.Name;
                user.Rank = userDTO.Rank ?? user.Rank;
                user.GateID = userDTO.GateID == 0 ? user.GateID : userDTO.GateID;
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "UpdateUser");
            }
            return response;
        }
        public async Task<ApiResponse<object>> LogIn(SignIn LogInDTO,IFormFile image)
        {
            var response = new ApiResponse<object>();
            var user = await _userManager.FindByNameAsync(LogInDTO.UserName);
            var printerMac = _context.PrinterMac.Where(u => u.PhoneMac == LogInDTO.PhoneMac).FirstOrDefault();
            if (user == null)
            {
                response.Status = false;
                response.Message = "This user is not exist";
                return response;
            }
            var roles = _userManager.GetRolesAsync(user).Result.ToList();
            if ((printerMac == null || printerMac.PrinterMac == null) && roles.Contains("Guard"))
            {
                if(printerMac == null)
                {
                    var printer = new PrinterMacs
                    {
                        PhoneMac = LogInDTO.PhoneMac,
                        PrinterMac = null
                    };
                    await _context.AddAsync(printer);
                    await _context.SaveChangesAsync();
                }
                response.Status = false;
                response.Message = "No Printer Realated";
                return response;
            }
            var signin = _context.SignInLog.Any(s => s.GardUserId == user.Id && s.LogOutDate == null);
            if(signin)
            {
                response.Status = false;
                response.Message = "This User Is Active In Another Device";
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(user, LogInDTO.Password, false, false);
            var jwtSecurityToken = await _services.GenerateJwt(user);
            if (!result.Succeeded)
            {
                response.Status = false;
                response.Message = "failure during signning in";
                return response;
            }
            var userdto = new UserDTO.Response();
            if (roles.Contains("Guard"))
            {
                userdto = _context.Users.Include(l => l.Gate)
                    .Where(u => u.Id == user.Id).Select(_mapper.Map<UserDTO.Response>).FirstOrDefault();
                userdto.Balance = _context.GateLog.Include(g => g.PrinterLogs).Where(g => g.UserId == user.Id && !g.IsPayed && !g.IsDeleted).ToList()
                    .Sum(g => g.Total + g.PrinterLogs.Where(p => !p.IsDeleted && !p.IsPayed).Sum(p => p.Price));
                userdto.PrinterMac = printerMac.PrinterMac;
            }
            userdto.Id = user.Id;    
            userdto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var reasons = _gateInterface.GetReasons().Response;
            var ParkTypes = _context.Owner.Skip(2).Select(o => o.Type).ToList();
            ParkTypes.AddRange(_context.InvitationType.Select(i=>i.Type));
            await _services.SaveLogIn(user.Id, image);
            response.Status = true;
            response.Message = "Success";
            response.Response = new
            {
                user = userdto,
                PrintReasons = reasons,
                Roles = roles,
                parkTypes = ParkTypes
            };
            return response;
        }
        public async Task<ApiResponse<bool>> LogOut(string UserId)
        {
            var response = new ApiResponse<bool>();
            var lastlog = _context.SignInLog.Where(u => u.GardUserId == UserId && u.LogOutDate == null).ToList();
            if (lastlog.Count == 0)
            {
                response.Status = false;
                response.Message = "incorrect user";
                return response;
            }
            foreach(var log in lastlog)
            {
                log.LogOutTime = DateTime.Now.ToString("hh: mm tt");
                log.LogOutDate = DateTime.Today;
            }
            
            
            _context.SignInLog.UpdateRange(lastlog);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "success";
            response.Response = true;
            return response;
        }
        public ApiResponse<List<DarUserDTO>> GetUsers()
        {
            var response = new ApiResponse<List<DarUserDTO>>();
            var users = _userManager.Users.Select(_mapper.Map<DarUserDTO>).ToList();
            if (users == null)
            {
                response.Status = false;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = users;
            return response;
        }
        public ApiResponse<List<DarUserDTO>> GetOnlineUsers()
        {
            var response = new ApiResponse<List<DarUserDTO>>();
            var users = _context.SignInLog.Include(s => s.GardUser).Where(s => s.LogOutDate == null)
                .Select(s => new DarUserDTO
                {
                    Id = s.GardUserId,
                    GateID = s.GardUser.GateID,
                    Name = s.GardUser.Name,
                    UserName = s.GardUser.UserName,
                    Rank = s.GardUser.Rank,
                    UID = s.GardUser.UID
                }).ToList();
            if (users == null)
            {
                response.Status = false;
                response.Message = "No Online Users";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = users;
            return response;
        }
        public async Task<ApiResponse<bool>> LogOutAllDevices(string UserId)
        {
            var response = new ApiResponse<bool>();
            var lastlogs = _context.SignInLog.Where(u => u.GardUserId == UserId && u.LogOutDate == null).ToList();
            if (lastlogs == null)
            {
                response.Status = false;
                response.Message = "incorrect user";
                return response;
            }
            foreach (var log in lastlogs)
            {
                log.LogOutTime = DateTime.Now.ToString("hh: mm tt");
                log.LogOutDate = DateTime.Today;
            }

            _context.SignInLog.UpdateRange(lastlogs);
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Message = "success";
            response.Response = true;
            return response;
        }
        #endregion

    }
}
