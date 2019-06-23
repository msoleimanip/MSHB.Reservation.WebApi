﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class ReportController : BaseController
    {
        private IReportService _reportService;
       

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
            _reportService.CheckArgumentIsNull(nameof(_reportService));
           
        }
        [HttpGet("[action]"), HttpPost("[action]")]
        public async Task<IActionResult> GetDashboardReport()
        {            
            return Ok(GetRequestResult(await _reportService.GetDashboardReport()));

        }
    }
}