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
    public class HoursController : ControllerBase
    {
        private readonly IHoursInterface _repo;

        public HoursController(IHoursInterface repo)
        {
            _repo = repo;
        }

        #region MyRegion

        [HttpPost]
        [Route("CheckInHour")]
        public async Task<IActionResult> CheckInHourPark([FromForm] string UserId, IFormFile image1, IFormFile image2)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.CheckInHour(UserId, image1, image2));
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("ConfirmCheckOutHour")]
        public async Task<IActionResult> ConfirmCheckOutHour(int Id, string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ConfirmCheckOutHour(Id,UserId));
            }
            return BadRequest();
        }
        #endregion
    }
}
