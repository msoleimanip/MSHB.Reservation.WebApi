using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
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
    public class CityController : BaseController
    {
        private ICityService _CityService;

        public CityController(ICityService CityService)
        {
            _CityService = CityService;
            _CityService.CheckArgumentIsNull(nameof(_CityService));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get([FromQuery] long Id)
        {
            return Ok(GetRequestResult(await _CityService.GetAsync(HttpContext.GetUser(),Id)));
        }


        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetCityByUser()
        {           
            var Citys =await _CityService.GetCityByUserAsync(HttpContext.GetUser());
            return Ok(GetRequestResult(Citys));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetUserCityForUser([FromQuery] Guid userId)
        {
            var Citys = await _CityService.GetUserCityForUserAsync(HttpContext.GetUser(), userId);
            return Ok(GetRequestResult(Citys));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> AddCity([FromBody] AddcityFormModel cityForm)
        {            
            var Citys = await _CityService.AddCityAsync(HttpContext.GetUser(), cityForm);
            return Ok(GetRequestResult(Citys));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> EditCity([FromBody] EditcityFormModel cityForm)
        {
            var Citys = await _CityService.EditCityAsync(HttpContext.GetUser(),cityForm);
            return Ok(GetRequestResult(Citys));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> DeactivateCity([FromBody] DeactivateCityFormModel cityForm)
        {
            var Citys = await _CityService.DeactivateCityAsync(HttpContext.GetUser(), cityForm);
            return Ok(GetRequestResult(Citys));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> DeleteCity([FromBody]
        [Required(ErrorMessage = "لیست اقامتگاه های ارسال شده برای حذف نامعتبر است")]List<long> CityIds)
        {
            var Citys = await _CityService.DeleteCityAsync(HttpContext.GetUser(),CityIds);
            return Ok(GetRequestResult(Citys));
        }
    }
}