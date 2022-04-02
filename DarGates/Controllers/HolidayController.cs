using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarGates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayInterface _repo;

        public HolidayController(IHolidayInterface repo)
        {
            _repo = repo;
        }
        #region OfficialHoliday
        [HttpPost]
        [Route("AddOfficialHoliday")]
        public async Task<IActionResult> AddOfficialHoliday(OfficialHolidayDTO officialHolidayDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddOfficialHoliday(officialHolidayDTO));
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("DeleteOfficialHoliday")]
        public async Task<IActionResult> DeleteOfficialHoliday(int ID,string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.DeleteOfficialHoliday(ID,UserId));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetOfficialHoliday")]
        public IActionResult GetOfficialHoliday()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetOfficialHoliday());
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("UpdateOfficialHoliday")]
        public async Task<IActionResult> UpdateOfficialHoliday(OfficialHolidayDTO officialHolidayDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateOfficialHoliday(officialHolidayDTO));
            }
            return BadRequest();
        }
        #endregion

        #region WeeklyHoliday
        [HttpGet]
        [Route("GetWeeklyHoliday")]
        public IActionResult GetWeeklyHoliday()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetWeeklyHoliday());
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("UpdateWeeklyHoliday")]
        public async Task<IActionResult> UpdateWeeklyHoliday(WeeklyHolidayDTO weeklyHolidayDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UpdateWeeklyHoliday(weeklyHolidayDTO));
            }
            return BadRequest();
        }
        #endregion
    }
}
