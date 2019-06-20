

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("GroupAuth_T")]

    public class GroupAuth :BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<GroupAuthRole> GroupRoles { get; set; }

    }
}
