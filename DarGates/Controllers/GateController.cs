using DarGates.DTOs;
using DarGates.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateController : ControllerBase
    {
        private readonly IGateInterface _repo;

        public GateController(IGateInterface repo)
        {
            _repo = repo;
        }

        #region Gate Operation
        [HttpPost]
        [Route("AddGate")]
        //[Authorize(Roles = "Guard")]
        public async Task<IActionResult> AddGate([FromBody] GateDTO gateDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddGate(gateDTO));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetAllGates")]
        //[Authorize(Roles = "Guard")]
        public IActionResult GetAllGates()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetAllGates());
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Bill")]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> Bill([FromBody] BillDTO billDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.Bill(billDTO));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CheckIn")]
        [Authorize(Roles ="Guard")]
        public async Task<IActionResult> CheckIn([FromForm] CheckInDTO.Request CheckInDTO, IFormFile file, IFormFile file1)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.CheckIn(CheckInDTO, file, file1));
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("CheckOut")]
        //[Authorize(Roles = "Guard")]
        public async Task<IActionResult> CheckOut([FromForm] string qrCode)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.CheckOut(qrCode));
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Cancel")]
        public async Task<IActionResult> Cancel([FromQuery] int LogId, string UserId)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.Cancel(LogId, UserId));
            }
            return BadRequest();
        }

        #endregion

        [HttpGet]
        [Route("Parked")]
        //[Authorize(Roles = "Guard")]
        public IActionResult Parked(string parkType,int pageNo)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.Parked(parkType, pageNo));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("ParkedForAdmin")]
        //[Authorize(Roles = "Guard")]
        public IActionResult ParkedForAdmin()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.ParkedForAdmin());
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("SummaryForToday")]
        //[Authorize(Roles = "Guard")]
        public IActionResult SummaryForParked()
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.SummaryForToday());
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("GetLogByID/{ID}")]
        //[Authorize(Roles = "Guard")]
        public IActionResult GetLogByID(int ID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetLogByID(ID));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetGateLogs")]
        public IActionResult GetGateLogs([FromQuery] int? Id, string ParkType, string InDate, string OutDate)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetGateLogs(Id, ParkType, InDate, OutDate));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("FilterLogs")]
        public IActionResult FilterLogs([FromQuery] string StartDate, string EndDate,string UserId,string ParkType,int PageNo)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.FilterLogs(StartDate, EndDate, UserId, ParkType, PageNo));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("ReportLogs")]
        public IActionResult ReportLogs([FromQuery] string StartDate, string EndDate, string UserId, string ParkType,int PageNo)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.ReportLogs(Convert.ToDateTime(StartDate), Convert.ToDateTime(EndDate), UserId, ParkType,PageNo));
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Print")]
        [Authorize(Roles = "Guard")]
        public async Task<IActionResult> Print(string UserID, int LogID, int ReasonID)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.Print(UserID, LogID, ReasonID));
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("UnPrint/{Id}")]
        public async Task<IActionResult> UnPrint(int Id)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.UnPrint(Id));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetUnPayedBills")]
        public IActionResult GetUnPayedBills(string UserID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetUnPayedBills(UserID));
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("PayInvoices")]
        public async Task<IActionResult> PayInvoices(PayInvoiceDTO payInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.PayInvoices(payInvoiceDTO));
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("ClearTheBalance")]
        public async Task<IActionResult> ClearTheBalance(string UserID)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.ClearTheBalance(UserID));
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("AddReason")]
        public async Task<IActionResult> AddReason(ReasonDTO reasonDTO)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _repo.AddReason(reasonDTO));
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetPrinterLog")]
        public IActionResult GetPrinterLog(string UserID)
        {
            if (ModelState.IsValid)
            {
                return Ok(_repo.GetPrinterLog(UserID));
            }
            return BadRequest();
        }
    }
}
