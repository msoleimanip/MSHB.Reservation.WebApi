using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ReportStructureFormModel
    {
        [Required(ErrorMessage=" بایستی شناسه گزارش درخواستی ارسال گردد.")]
        public string ReportId { get; set; }
    }
}
