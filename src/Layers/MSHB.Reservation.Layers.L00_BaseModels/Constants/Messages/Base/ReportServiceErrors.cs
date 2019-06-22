using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class ReportServiceErrors
    {
        public static readonly ReservationErrorMessage GetReportDashboard =
          new ReservationErrorMessage("GRD-1000", "خطا در دریافت اطلاعات داشبورد");
    }
}
