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
                    throw new ReservationGlobalException(AccommodationServiceErrors.AddDuplicateAccommodationError);
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationServiceErrors.AddAccommodationError, ex);
            }
        }

        public async Task<SearchAccommodationViewModel> GetAsync(SearchAccommodationFormModel searchAccommodationForm)
        {
            try
            {
                var queryable = _context.Accommodations.AsQueryable();

                if (!string.IsNullOrEmpty(searchAccommodationForm.Caption))
                {
                    queryable = queryable.Where(q => q.Caption.Contains(searchAccommodationForm.Caption));
                }

                if (searchAccommodationForm.IsActivated.HasValue)
                {
                    queryable = queryable.Where(q => q.IsActivated == searchAccommodationForm.IsActivated.Value);
                }

                if (searchAccommodationForm.CityId.HasValue)
                {
                    queryable = queryable.Where(q => q.CityId == searchAccommodationForm.CityId.Value);
                }


                queryable = queryable.OrderBy(x => x.CityId);

                var resp = await queryable.Skip((searchAccommodationForm.PageIndex - 1) * searchAccommodationForm.PageSize).Take(searchAccommodationForm.PageSize).ToListAsync();
                var count = await queryable.CountAsync();
                var searchViewModel = new SearchAccommodationViewModel
                {
                    searchAccommodationViewModels = resp.Select(respAcc => new AccommodationViewModel()
                    {
                        Id = respAcc.Id,
                        Caption = respAcc.Caption,
                        Address = respAcc.Address,
                        CityTitle = $"{respAcc.City.Title} ({respAcc.City.Province.Title})",
                        Code = respAcc.Code,
                        District = respAcc.District,
                        IsActivated = respAcc.IsActivated,
                        FileId = respAcc.FileId,
                        Number = respAcc.Number

                    }).ToList(),
                    PageIndex = searchAccommodationForm.PageIndex,
                    PageSize = searchAccommodationForm.PageSize,
                    TotalCount = count
                };
                return searchViewModel;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationServiceErrors.GetAccommodationError, ex);
            }

        }

        public async Task<bool> AddUnitAsync(AddUnitFormModel addUnitForm)
        {
            try
            {
                var unit = new Unit()
                {
                    AccommodationId = addUnitForm.AccommodationId,
                    AccommodationType = addUnitForm.AccommodationType,
                    DoubleBedCount = addUnitForm.DoubleBedCount,
                    IsActive = addUnitForm.IsActive,
                    MaximumCount = addUnitForm.MaximumCount,
                    MinimumCount = addUnitForm.MinimumCount,
                    RoomCount = addUnitForm.RoomCount,
                    SingleBedCount = addUnitForm.SingleBedCount
                };

                await _context.Units.AddAsync(unit);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationServiceErrors.AddUnitError, ex);
            }
        }

    }
}
