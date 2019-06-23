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
using MSHB.Reservation.Presentation.WebUI.filters;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class AccommodationRoomController : BaseController
    {
        private IAccommodationService _accommodationService;

        public AccommodationRoomController(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
            _accommodationService.CheckArgumentIsNull(nameof(_accommodationService));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> AddAccommodationRoom([FromBody] AddAccommodationRoomFormModel accommodationForm)
        {
            var resp = await _accommodationService.AddAccommodationRoom(HttpContext.GetUser(), accommodationForm);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> EditAccommodationRoom([FromBody] EditAccommodationRoomFormModel accommodationForm)
        {
            var resp = await _accommodationService.EditAccommodationRoom(HttpContext.GetUser(), accommodationForm);
            return Ok(GetRequestResult(resp));
        }
        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> DeleteAccommodationRoom([FromBody]  [Required(ErrorMessage = "لیست مکان های اقامتگاه ارسال شده برای حذف نامعتبر است")]List<long> accommodationFormIds)
        {
            var resp = await _accommodationService.DeleteAccommodationRoom(HttpContext.GetUser(), accommodationFormIds);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> GetUserAccommodationRoomForUser([FromBody] AccommodationRoomSearchFormModel accommodationForm)
        {
            var resp = await _accommodationService.GetUserAccommodationRoomForUserAsync(HttpContext.GetUser(), accommodationForm);
            return Ok(GetRequestResult(resp));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> DeactivateAccommodationRoom([FromBody] DeactivateAccommodationRoomFormModel accommodationForm)
        {
            var resp = await _accommodationService.DeactivateAccommodationRoomAsync(HttpContext.GetUser(), accommodationForm);
            return Ok(GetRequestResult(resp));
        }
    }
}