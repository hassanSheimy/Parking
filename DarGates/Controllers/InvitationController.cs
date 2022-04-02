using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationInterface _repo;

        public InvitationController(IInvitationInterface repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("AddInvitation")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddInvitation(InvitationDTO.AddInvitation invitationDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddInvitation(invitationDTO));
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteInvitation")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteInvitation(int Id,string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteInvitation(Id));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("ScanInvitationQr")]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> ScanInvitationQr([FromForm]string QrCode)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ScanInvitationQr(QrCode));
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("CheckInInvitation")]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckInInvitation([FromForm] CheckInInvitationDTO checkInInvitationDTO, IFormFile file, IFormFile file1)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.CheckInInvitation(checkInInvitationDTO, file, file1));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetInvitations")]
        //[Authorize(Roles ="Admin")]
        public IActionResult GetInvitations(string UserId, string From, string To)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetInvitations(UserId, From, To));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetInvitationTypes")]
        //[Authorize(Roles ="Admin")]
        public IActionResult GetInvitationTypes()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetInvitationTypes());
            }
            return BadRequest();
        }
    }
}
