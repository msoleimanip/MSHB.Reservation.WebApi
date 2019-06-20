using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class UserEquipmentAssignFormModel
    {
        [Required(ErrorMessage = "باید حتما کاربر انتخاب گردد")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "باید حتما تجهیزات انتخاب گردد"), MinLength(1)]
        public List<long> EquipmentIds { get; set; }

    }
}
