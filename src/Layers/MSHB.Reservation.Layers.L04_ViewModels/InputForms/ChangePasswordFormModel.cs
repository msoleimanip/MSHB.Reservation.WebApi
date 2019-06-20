using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ChangePasswordFormModel
    {
        [Required(ErrorMessage = "انتخاب کاربر الزامی است")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "پسورد جاری الزامی است")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "پسورد جدید الزامی است")]
        public string NewPassword { get; set; }
    }
}
