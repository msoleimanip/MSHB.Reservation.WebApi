using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("Role_T")]

    public class Role:BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Discriminator { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
