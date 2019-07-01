using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class ReservationRoomViewModel
    {
        public long Id { get; set; }
        public string RoomNumber { get; set; }
        public long RoomPrice { get; set; }
        public int? BedRoom { get; set; }
        public RoomType RoomType { get; set; }
        public int Rank { get; set; }
        public int Bed { get; set; }
        public bool? IsActivated { get; set; } = true;
        public int? Capacity { get; set; }
        public long AccommodationRoomId { get; set; }
        public long CityId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? EntranceTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string NationalCode { get; set; }
        public string PhoneNumber { get; set; }
        public GenderType GenderType { get; set; }
        public string PersonalCode { get; set; }
        public long SystemCode { get; set; }
        public int GuestCounts { get; set; }
        public string Description { get; set; }
        public long PriceAccommodation { get; set; }
        public string UsernameAssignment { get; set; }
        public StatusReservationType Status { get; set; }
        public Guid? FileId { get; set; }

    }
}
