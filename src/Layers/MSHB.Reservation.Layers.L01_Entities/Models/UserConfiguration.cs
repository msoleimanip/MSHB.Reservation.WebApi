using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSHB.Reservation.Layers.L01_Entities.Models
{
    [Table("UserConfiguration_T")]

    public class UserConfiguration : BaseEntity
    {
        public Guid UserId { get; set; }
        [NotMapped]
        private string _Configuration { get; set; }

        public string Configuration {
            get
            {
                return JsonConvert.DeserializeObject<string>(_Configuration);
            }
            set
            {
                _Configuration = JsonConvert.SerializeObject(value);
            }
        }
        public virtual User User { get; set; }
    }
}
