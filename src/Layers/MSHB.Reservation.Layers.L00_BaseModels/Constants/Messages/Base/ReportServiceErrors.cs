using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base
{
    public class ReportServiceErrors
    {
        public static readonly ReservationErrorMessage GetReportDashboard =
          new ReservationErrorMessage("GRD-1000", "خطا در دریافت اطلاعات داشبورد");
        public static readonly ReservationErrorMessage GetReportStructure =
          new ReservationErrorMessage("GRD-1001", "خطا در دریافت ساختار گزارش");
        public static readonly ReservationErrorMessage ReportStructureNotFound =
         new ReservationErrorMessage("GRD-1002", "گزارشی که به دنبال آن هستید در سیستم وجود ندارد");



    }
}
