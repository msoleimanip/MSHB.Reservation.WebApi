using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class SmsService:ISmsService
    {
        private readonly ReservationDbContext _context;


        public SmsService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));

        }
    }
}