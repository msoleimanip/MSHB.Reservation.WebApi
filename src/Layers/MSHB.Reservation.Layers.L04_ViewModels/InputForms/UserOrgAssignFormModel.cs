using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class UserCityAssignFormModel
    {
        [Required(ErrorMessage = "باید حتما شهر مورد نظر انتخاب شود")]
        public long CityId { get; set; }
        [Required(ErrorMessage = "باید حتما کاربر انتخاب گردد")]
        public Guid UserId { get; set; }
    }
}
