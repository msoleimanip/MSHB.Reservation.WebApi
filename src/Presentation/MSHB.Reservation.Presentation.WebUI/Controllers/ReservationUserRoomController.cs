using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Presentation.WebCore;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ReservationUserRoomController : BaseController
    {
        private IReservationUserRoomService _reservationService;

        public ReservationUserRoomController(IReservationUserRoomService reservationUserRoomService)
        {
            _reservationService = reservationUserRoomService;
            _reservationService.CheckArgumentIsNull(nameof(_reservationService));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> AddReservationRoom([FromBody] AddReservationRoomFormModel reservationForm)
        {
            var resp = await _reservationService.AddReservationRoomAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> EditReservationRoom([FromBody] EditReservationRoomFormModel reservationForm)
        {
            var resp = await _reservationService.EditReservationRoomAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }
        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> DeleteReservationRoom([FromBody]  [Required(ErrorMessage = "لیست رزروهای اقامتگاه ارسال شده برای حذف نامعتبر است")]List<long> reservationFormIds)
        {
            var resp = await _reservationService.DeleteReservationRoomAsync(HttpContext.GetUser(), reservationFormIds);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetUsereservationRoomForUser([FromQuery] ReservationRoomSearchFormModel reservationForm)
        {
            var resp = await _reservationService.GetUserReservationRoomForUserAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }
    }
}