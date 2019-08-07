using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L01_Entities.Enums;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class AccommodationService : IAccommodationService
    {
        private readonly ReservationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;


        public AccommodationService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<bool> AddAsync(AddAccommodationFormModel accommodationForm)
        {
            try
            {
                if (!(await _context.Accommodations.AnyAsync(x => x.Caption == accommodationForm.Caption)))
                {
                    var accommodation = new Accommodation()
                    {
                        Caption = accommodationForm.Caption,
                        AccommodationType = accommodationForm.AccommodationType,
                        Address = accommodationForm.Address,
                        CityId = accommodationForm.CityId,
                        Code = accommodationForm.Code,
                        District = accommodationForm.District,
                        FileId = accommodationForm.FileId,
                        IsActivated = accommodationForm.IsActivated,
                        Number = accommodationForm.Number
                    };

                    await _context.Accommodations.AddAsync(accommodation);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                    throw new ReservationGlobalException(AccommodationRoomServiceErrors.AddDuplicateAccommodationError);
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.AddAccommodationError, ex);
            }
        }

        public Task<bool> GetAsync(SearchAccommodationFormModel searchAccommodationForm)
        {
            throw new NotImplementedException();
        }
    }
}
