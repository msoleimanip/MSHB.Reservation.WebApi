using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("RuleState_T")]

    public class RuleState : BaseEntity
    {
        public string Title { get; set; }

        public string Key { get; set; }

        public int? OrderIndex { get; set; }

        public bool? IsDefault { get; set; }
    }
}
