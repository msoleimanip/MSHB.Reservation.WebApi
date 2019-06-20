using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditGroupFormModel : AddGroupFormModel
    {
        [Required(ErrorMessage = "شناسه گروه وارد نشده است")]
        public long GroupId { get; set; }
    }
}
