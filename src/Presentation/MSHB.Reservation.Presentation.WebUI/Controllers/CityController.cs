using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Presentation.WebCore;
using MSHB.Reservation.Presentation.WebUI.filters;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class CityController : BaseController
    {
        private readonly IMemoryCache _cache;
        private ICityService _CityService;

        public CityController(ICityService CityService, IMemoryCache memoryCache)
        {
            _CityService = CityService;
            _CityService.CheckArgumentIsNull(nameof(_CityService));

            _cache = memoryCache;
            _cache.CheckArgumentIsNull(nameof(_cache));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCities()
        {
            List<CityViewModel> cities = new List<CityViewModel>();
            cities = _cache.Get<List<CityViewModel>>("dashboard");
            if (cities is null || cities.Count == 0)
            {
                cities = await _CityService.GetAllAsync();
                _cache.Set("dashboard", cities, TimeSpan.FromHours(1));
            }

            return Ok(GetRequestResult(cities));
        }

        [HttpGet("[action]/{ProvinceId}")]
        public async Task<IActionResult> GetCities(long ProvinceId)
        {
            return Ok(GetRequestResult(await _CityService.GetAsync(ProvinceId)));
        }


        [HttpGet("[action]/{ProvinceId}/{Id}")]
        public async Task<IActionResult> GetCity(long ProvinceId, long Id)
        {
            return Ok(GetRequestResult(await _CityService.GetAsync(ProvinceId, Id)));
        }

    }
}