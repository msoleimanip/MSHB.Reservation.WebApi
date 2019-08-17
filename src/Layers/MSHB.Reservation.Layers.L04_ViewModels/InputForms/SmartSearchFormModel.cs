using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class SmartSearchFormModel : SearchModel
    {
        [Required]
        public int CityId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
