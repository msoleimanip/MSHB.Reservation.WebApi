using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditcityFormModel : AddcityFormModel
    {
        [Required(ErrorMessage = "شناسه شهر وارد نشده است")]
        public long CityId { get; set; }
    }
}
