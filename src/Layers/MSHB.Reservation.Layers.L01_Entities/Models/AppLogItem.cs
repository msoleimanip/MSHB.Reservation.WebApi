

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("Log_T")]

    public class AppLogItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { set; get; }
        public int EventId { get; set; }
        public string Url { get; set; }
        public string LogLevel { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string StateJson { get; set; }
        public bool IsSoftDeleted { get; set; } = false;
        public DateTimeOffset CreatedDateTime { get; set; }
    }
}
