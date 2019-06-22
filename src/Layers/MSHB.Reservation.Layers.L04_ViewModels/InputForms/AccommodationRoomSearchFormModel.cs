using MSHB.Reservation.Layers.L00_BaseModels.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AccommodationRoomSearchFormModel: SearchModel
    {
       
        public bool? IsActivated { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? BedRoom { get; set; }     
        public int? Bed { get; set; }
        public int? Capacity { get; set; }
        [Required(ErrorMessage = "بایستی ابتدا اقامتگاه مشخص شود")]
        public long CityId { get; set; }
    }
}
