using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Presentation.WebUI.filters;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Presentation.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookinationController : BaseController
    {
        private IBookinationService _bookinationService;

        public BookinationController(IBookinationService bookinationService)
        {
            _bookinationService = bookinationService;
            _bookinationService.CheckArgumentIsNull(nameof(_bookinationService));
        }


        [HttpGet("[action]"), HttpPost("[action]")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Add([FromBody] AddBookinationFormModel addFormModel)
        {
            return Ok(GetRequestResult(await _bookinationService.AddAsync(addFormModel)));
        }

    }
}