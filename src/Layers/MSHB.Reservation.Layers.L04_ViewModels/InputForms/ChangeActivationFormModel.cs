using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ChangeActivationFormModel
    {
        [Required(ErrorMessage = "انتخاب کاربر الزامی است")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "وضعیت الزامی است")]
        public bool IsActive { get; set; }
    }
}
