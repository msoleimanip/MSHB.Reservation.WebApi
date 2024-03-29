﻿using MSHB.Reservation.Layers.L00_BaseModels.Search;
using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class SearchAccommodationFormModel : SearchModel
    {
        public string Caption { get; set; }
        public AccommodationType? AccommodationType { get; set; }
        public string Code { get; set; }
        public bool? IsActivated { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public long? CityId { get; set; }
    }
}
