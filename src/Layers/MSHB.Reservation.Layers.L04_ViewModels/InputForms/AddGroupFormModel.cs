using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddGroupFormModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<long> RoleIds { get; set; }

    }
}
