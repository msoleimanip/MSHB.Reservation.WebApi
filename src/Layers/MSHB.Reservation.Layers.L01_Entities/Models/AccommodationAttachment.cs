﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("AccommodationAttachment_T")]
    public class AccommodationAttachment : BaseEntity
    {
        public long AccommodationId { get; set; }
        [MaxLength(20)]
        public string FileType { get; set; }
        public long? FileSize { get; set; }
        public Guid? FileId { get; set; }
        public virtual Accommodation Accommodation { get; set; }
    }
}
