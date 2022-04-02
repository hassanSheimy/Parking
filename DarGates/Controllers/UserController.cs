using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _repo;

        public UserController(IUserInterface repo)
        {
            _repo = repo;
        }
        #region User End Points

        [HttpPost]
        [Route("AddAdmin")]
        public async Task<IActionResult> AddAdmin(UserDTO.Request userDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.RegisterAdmin(userDTO));
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("AddManager")]
        public async Task<IActionResult> AddManager(UserDTO.Request userDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.RegisterManager(userDTO));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(UserDTO.Request userDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.RegisterGuardUser(userDTO));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser(DarUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateUser(userDTO));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromForm]SignIn LogInDTO, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.LogIn(LogInDTO, file));
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("SignOut")]
        public async Task<IActionResult> SignOut(string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.LogOut(UserId));
            }
            return BadRequest();
        }
        
        [HttpPut]
        [Route("LogOutAllDevices")]
        public async Task<IActionResult> LogOutAllDevices(string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.LogOutAllDevices(UserId));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetUsers());
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetOnlineUsers")]
        public IActionResult GetOnlineUsers()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetOnlineUsers());
            }
            return BadRequest();
        }
        #endregion
    }
}
