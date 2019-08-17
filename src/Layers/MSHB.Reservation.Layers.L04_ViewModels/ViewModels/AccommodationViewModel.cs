using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class AccommodationViewModel
    {
        public long Id { get; set; }
        public string Caption { get; set; }
        public string Code { get; set; }
        public bool IsActivated { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public Guid? FileId { get; set; }
        public string CityTitle { get; set; }
    }
}
