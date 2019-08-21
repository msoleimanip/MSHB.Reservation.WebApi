using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationDbContext _context;


        public ReservationService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<bool> AddAsync(AddReservationFormModel AddFomrModel)
        {
            try
            {
                if (!(await _context.Units.AnyAsync(x => x.Id == AddFomrModel.UnitId)))
                    throw new ReservationGlobalException(ReservationServiceErrors.UnitNotFoundError);

                return true;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ReservationServiceErrors.AddError, ex);
            }
        }
    }
}
