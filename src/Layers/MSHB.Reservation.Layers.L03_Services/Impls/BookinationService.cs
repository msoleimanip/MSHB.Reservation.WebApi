using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L01_Entities.Models;
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
    public class BookinationService : IBookinationService
    {
        private readonly ReservationDbContext _context;


        public BookinationService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<bool> AddAsync(AddBookinationFormModel AddFomrModel)
        {
            try
            {
                if (!(await _context.Units.AnyAsync(x => x.Id == AddFomrModel.UnitId)))
                    throw new ReservationGlobalException(BookinationServiceErrors.UnitNotFoundError);

                var bookination = new Bookination()
                {
                    Description = AddFomrModel.Description,
                    Email = AddFomrModel.Email,
                    EndDate = AddFomrModel.EndDate,
                    FirstName = AddFomrModel.FirstName,
                    LastName = AddFomrModel.LastName,
                    Mobile = AddFomrModel.Mobile,
                    StartDate = AddFomrModel.StartDate,
                    UnitId = AddFomrModel.UnitId
                };

                await _context.Bookinations.AddAsync(bookination);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(BookinationServiceErrors.AddError, ex);
            }
        }
    }
}
