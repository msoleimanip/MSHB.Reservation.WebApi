using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("ReportStructure_T")]
    public class ReportStructure:BaseEntity
    {
        [Required]
        [MaxLength(40)]
        public string ReportId { get; set; }
        [MaxLength]
        public string Configuration { get; set; }
        public string ProtoType { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
