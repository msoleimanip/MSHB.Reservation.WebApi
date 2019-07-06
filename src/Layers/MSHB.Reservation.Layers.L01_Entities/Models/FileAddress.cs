using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("FileAddress_T")]
    public class FileAddress
    {
        public Guid FileId { get; set; }
        [MaxLength(20)]
        public string FileType { get; set; }
        public long? FileSize { get; set; }
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
