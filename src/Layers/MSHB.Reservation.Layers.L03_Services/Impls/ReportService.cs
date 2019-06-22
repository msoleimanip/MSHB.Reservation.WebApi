using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class ReportService: IReportService
    {
        private readonly ReservationDbContext _context;

        public ReportService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }
    }
}
