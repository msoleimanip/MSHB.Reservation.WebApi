using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditUserFormModel : AddUserFormModel
    {
        [Required(ErrorMessage = "شناسه کاربر وارد نشده است")]
        public Guid UserId { get; set; }
      

    }
}
