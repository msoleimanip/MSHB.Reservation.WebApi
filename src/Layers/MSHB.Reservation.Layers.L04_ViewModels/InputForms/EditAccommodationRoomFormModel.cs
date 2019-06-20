﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditAccommodationRoomFormModel: AddAccommodationRoomFormModel
    {
        [Required(ErrorMessage = "شناسه مکان اقامتگاه وارد نشده است")]
        public long AccommodationRoomId { get; set; }
    }
}
