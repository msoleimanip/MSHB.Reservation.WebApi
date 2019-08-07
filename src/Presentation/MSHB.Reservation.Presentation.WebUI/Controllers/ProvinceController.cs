using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : BaseController
    {
        private IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
            _provinceService.CheckArgumentIsNull(nameof(_provinceService));
        }


        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetProvinces()
        {
            return Ok(GetRequestResult(await _provinceService.GetAsync()));
        }


        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProvince(long Id)
        {
            return Ok(GetRequestResult(await _provinceService.GetAsync(Id)));
        }
    }
}