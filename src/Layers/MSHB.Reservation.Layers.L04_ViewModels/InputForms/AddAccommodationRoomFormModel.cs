using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddAccommodationRoomFormModel
    {
        [Required(ErrorMessage = "باید برای مکان اقامتگاه، شماره یا نام انتخاب کنید"),MinLength(1)]
        public string RoomNumber { get; set; }
        [Required(ErrorMessage = "باید برای مکان اقامتگاه، قیمت لحاظ کنید.")]
        public long RoomPrice { get; set; }       
        public int? BedRoom { get; set; }
        public bool? IsEmpty { get; set; } 
        public int RoomType { get; set; }
        public int Rank { get; set; }
        [Required(ErrorMessage = "باید برای مکان اقامتگاه، تعداد تخت لحاظ کنید.")]
        public int Bed { get; set; }
        public bool? IsActivated { get; set; } 
        public int? Capacity { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "باید اقامتگاه  گردد.")]
        public long? CityId { get; set; }
    }
}
