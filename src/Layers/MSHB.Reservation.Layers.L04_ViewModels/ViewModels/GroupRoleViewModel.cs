using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class GroupRoleViewModel
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<long> RoleIds { get; set; }

    }
}
