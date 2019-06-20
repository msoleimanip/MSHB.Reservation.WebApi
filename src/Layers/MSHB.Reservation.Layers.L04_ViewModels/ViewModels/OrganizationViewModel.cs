﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.ViewModels
{
    public class CityViewModel
    {
        public long Id { get; set; }
        public string CityName { get; set; }

        public string Description { get; set; }

        public long? ParentId { get; set; }
    }
}
