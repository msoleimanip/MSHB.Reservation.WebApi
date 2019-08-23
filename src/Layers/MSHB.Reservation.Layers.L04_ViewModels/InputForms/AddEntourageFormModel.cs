using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddEntourageFormModel
    {
        public long BookinationId { get; set; }
        public GenderType GenderType { get; set; }
        public string Name { get; set; }
        public string NationalityCode { get; set; }
        public string Relative { get; set; }
        public int Age { get; set; }
    }
}
