using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestInterface _repo;

        public GuestController(IGuestInterface repo)
        {
            _repo = repo;
        }
        #region Owners / Guests
        [HttpPost]
        [Route("AddOwner")]
        public async Task<IActionResult> AddOwner([FromBody] OwnerDTO ownerDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOwner(ownerDTO));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateOwner")]
        public async Task<IActionResult> UpdateOwner([FromBody] OwnerDTO ownerDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateOwner(ownerDTO));
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteOwner")]
        public async Task<IActionResult> DeleteOwner([FromQuery]int ID,string UserID)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteOwner(ID, UserID));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetOwners")]
        //[Authorize(Roles = "Guard")]
        public IActionResult GetOwners()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetOwners());
            }
            return BadRequest();
        }
        #endregion
    }
}
