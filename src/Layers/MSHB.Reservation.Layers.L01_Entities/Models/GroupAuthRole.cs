

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSHB.Reservation.Layers.L01_Entities.Models;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("GroupAuthRole_T")]

    public class GroupAuthRole :BaseEntity
    {
        
        
        public long? GroupAuthId { get; set; }

        public virtual GroupAuth GroupAuth { get; set; }


        [Required]
        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
