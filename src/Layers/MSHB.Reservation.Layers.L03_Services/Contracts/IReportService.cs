using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IReportService
    {
        Task<DashboardReportViewModel> GetDashboardReport();
        Task<ReportStructureViewModel> GetReportStructureAsync(ReportStructureFormModel reportStructureFormModel);
    }
}
