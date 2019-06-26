using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Presentation.WebCore;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(Roles = "Upload")]
    public class UploadController : BaseController
    {

        private IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
            _uploadService.CheckArgumentIsNull(nameof(_uploadService));
        }

        [HttpGet("[action]"), HttpPost("[action]")]
        [Authorize(Roles = "Upload-UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            return Ok(GetRequestResult(await _uploadService.UploadFileAsync(HttpContext.GetUser(), file)));
        }

    }
}