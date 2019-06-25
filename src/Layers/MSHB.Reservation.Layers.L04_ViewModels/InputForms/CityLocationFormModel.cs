using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class CityImagesFormModel
    {
        [Required(ErrorMessage = "شناسه اقامتگاه وارد نشده است")]
        public long CityId { get; set; }
        public List<Guid> UploadFiles { get; set; }
    }
}
