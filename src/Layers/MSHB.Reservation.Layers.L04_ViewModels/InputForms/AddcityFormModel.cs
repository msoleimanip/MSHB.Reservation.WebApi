using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class AddcityFormModel
    {
        
        [Required(ErrorMessage = "باید برای اقامتگاه جدید، نام انتخاب کنید")]
        public string CityName { get; set; }
        public string Description { get; set; }       
        public long? ParentId { get; set; }
    }
}
