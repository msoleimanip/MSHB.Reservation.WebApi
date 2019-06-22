using Microsoft.Extensions.Caching.Memory;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class ReportService: IReportService
    {
        private readonly ReservationDbContext _context;
        private IMemoryCache _cache;
        public ReportService(ReservationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
            _cache = memoryCache;
        }

        public Task<DashboardReportViewModel> GetDashboardReport()
        {
            throw new NotImplementedException();
        }
    }
}
