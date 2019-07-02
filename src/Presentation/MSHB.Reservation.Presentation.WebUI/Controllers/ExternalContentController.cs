using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ExternalContentController : BaseController
    {
        private IReservationUserRoomService _reservationUserRoomService;

        public ExternalContentController(IReservationUserRoomService reservationUserRoomService)
        {
            _reservationUserRoomService = reservationUserRoomService;
            _reservationUserRoomService.CheckArgumentIsNull(nameof(_reservationUserRoomService));
        
        }
        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> ReceiveContent([FromBody] MessageContent messageContent)
        {
            var resp = await _reservationUserRoomService.ReceiveContentAsync(messageContent);
            return Ok();
        }
    }
}