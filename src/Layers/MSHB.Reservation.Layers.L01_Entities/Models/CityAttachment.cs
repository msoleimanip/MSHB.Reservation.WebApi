﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    public class CityAttachment : BaseEntity
    {
        public long CityId { get; set; }

        [MaxLength(20)]
        public string FileType { get; set; }
        public long? FileSize { get; set; }
        public string FilePath { get; set; }
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
    }
}
