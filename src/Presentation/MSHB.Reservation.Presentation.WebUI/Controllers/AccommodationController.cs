using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = "Accommodation")]
    public class AccommodationController : BaseController
    {
        private IAccommodationService _accommodationService;

        public AccommodationController(IAccommodationService accommodationService)
        {
            _accommodationService = accommodationService;
            _accommodationService.CheckArgumentIsNull(nameof(_accommodationService));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        //[Authorize(Roles = "Accommodation-AddAccommodation")]
        public async Task<IActionResult> AddAccommodation([FromBody] AddAccommodationFormModel accommodationForm)
        {
            return Ok(GetRequestResult(await _accommodationService.AddAsync(accommodationForm)));
        }


        [HttpPost("[action]")]
        [ValidateModelAttribute]
        //[Authorize(Roles = "Accommodation-Get")]
        public async Task<IActionResult> Get([FromBody] SearchAccommodationFormModel searchAccommodationForm)
        {
            return Ok(GetRequestResult(await _accommodationService.GetAsync(searchAccommodationForm)));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> AddUnit([FromBody] AddUnitFormModel addUnitForm)
        {
            return Ok(GetRequestResult(await _accommodationService.AddUnitAsync(addUnitForm)));
        }


        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> SmartSearch([FromBody] SmartSearchFormModel smartSearchForm)
        {
            return Ok(GetRequestResult(await _accommodationService.SmartSearchAsync(smartSearchForm)));
        }


        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> GetAccommodationUnits([FromBody] AccommodationUnitFormModel accommodationUnitForm)
        {
            return Ok(GetRequestResult(await _accommodationService.GetAccommodationUnitsAsync(accommodationUnitForm)));
        }

        [HttpGet("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> GetUnit([FromQuery] long UnitId)
        {
            return Ok(GetRequestResult(await _accommodationService.GetUnitAsync(UnitId)));
        }

        [HttpGet("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> GetAccommodationAttachments([FromQuery] long AccommodationId)
        {
            return Ok(GetRequestResult(await _accommodationService.GetAccommodationAttachmentsAsync(AccommodationId)));
        }


        [HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> SetAccommodationAttachments([FromBody] AccommodationAttachmentsFormModel accommodationAttachmentsForm)
        {
            return Ok(GetRequestResult(await _accommodationService.SetAccommodationAttachmentsAsync(accommodationAttachmentsForm)));
        }

        [HttpGet("[action]")]
        [ValidateModelAttribute]
        [AllowAnonymous]
        public async Task<IActionResult> GetAttachments([FromQuery] long AccommodationId)
        {
            return Ok(GetRequestResult(await _accommodationService.GetAttachmentsAsync(AccommodationId)));
        }

        [HttpPost("[action]")]
        [ValidateModelAttribute]
        //[Authorize(Roles = "City-DeleteAttachmentCity")]
        public async Task<IActionResult> DeleteAccommodationAttachment([FromBody]DeleteAttachmentFormModel DeleteAttachmentForm)
        {            
            return Ok(GetRequestResult(await _accommodationService.DeleteAccommodationAttachmentAsync(DeleteAttachmentForm)));
        }

    }
}