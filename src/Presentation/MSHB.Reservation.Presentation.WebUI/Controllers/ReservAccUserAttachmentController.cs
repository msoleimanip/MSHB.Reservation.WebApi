using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Presentation.WebCore;
using MSHB.Reservation.Presentation.WebUI.filters;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ReservationUserAttachment")]
    public class ReservAccUserAttachmentController: BaseController
    {
        private IAccommodationUserAttachmentService _accommodationUserAttachmentServiceService;

        public ReservAccUserAttachmentController(IAccommodationUserAttachmentService accommodationUserAttachmentServiceService)
        {
            _accommodationUserAttachmentServiceService = accommodationUserAttachmentServiceService;
            _accommodationUserAttachmentServiceService.CheckArgumentIsNull(nameof(_accommodationUserAttachmentServiceService));
        }

           [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        [Authorize(Roles = "ReservationUserAttachment-AddReservationUserAttachment")]
        public async Task<IActionResult> AddReservationUserAttachmentRoom([FromBody] List<AddReservationUserAttachmentFormModel> reservationForm)
        {
            var resp = await _accommodationUserAttachmentServiceService.AddReservationUserAttachmentRoomAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        [Authorize(Roles = "ReservationUserAttachment-EditReservationUserAttachment")]
        public async Task<IActionResult> EditReservationUserAttachmentRoom([FromBody] EditReservationUserAttachmentFormModel reservationForm)
        {
            var resp = await _accommodationUserAttachmentServiceService.EditReservationUserAttachmentRoomAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }
        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        [Authorize(Roles = "ReservationUserAttachment-DeleteReservationUserAttachment")]
        public async Task<IActionResult> DeleteReservationUserAttachmentRoom([FromBody]  [Required(ErrorMessage = "لیست همراهان ارسال شده برای حذف نامعتبر است")]List<long> reservationFormIds)
        {
            var resp = await _accommodationUserAttachmentServiceService.DeleteReservationUserAttachmentAsync(HttpContext.GetUser(), reservationFormIds);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        [Authorize(Roles = "ReservationUserAttachment-GetReservationUserAttachment")]
        public async Task<IActionResult> GetReservationUserAttachment([FromQuery] ReservationUserAttachmentSearchFormModel reservationForm)
        {
            var resp = await _accommodationUserAttachmentServiceService.GetReservationUserAttachmentAsync(HttpContext.GetUser(), reservationForm);
            return Ok(GetRequestResult(resp));
        }
        
    }
}