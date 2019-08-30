using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<long> AddAsync(AddBookinationFormModel AddFomrModel)
        {
            try
            {
                if (!(await _context.Units.AnyAsync(x => x.Id == AddFomrModel.UnitId)))
                    throw new ReservationGlobalException(BookinationServiceErrors.UnitNotFoundError);

                var bookination = new Bookination()
                {
                    Description = AddFomrModel.Description,
                    NationalityCode = AddFomrModel.NationalityCode,
                    EndDate = AddFomrModel.EndDate,
                    FirstName = AddFomrModel.FirstName,
                    LastName = AddFomrModel.LastName,
                    Mobile = AddFomrModel.Mobile,
                    StartDate = AddFomrModel.StartDate,
                    UnitId = AddFomrModel.UnitId,
                    ReserveCode = Guid.NewGuid()
                };

                await _context.Bookinations.AddAsync(bookination);
                await _context.SaveChangesAsync();

                return bookination.Id;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(BookinationServiceErrors.AddError, ex);
            }
        }

        public async Task<SearchBookinationViewModel> GetAsync(SearchBookinationFormModel searchFormModel)
        {
            try
            {
                var queryable = _context.Bookinations.AsQueryable();

                if (!string.IsNullOrEmpty(searchFormModel.FirstName))
                {
                    queryable = queryable.Where(q => q.FirstName.Contains(searchFormModel.FirstName));
                }

                if (!string.IsNullOrEmpty(searchFormModel.LastName))
                {
                    queryable = queryable.Where(q => q.LastName.Contains(searchFormModel.LastName));
                }

                if (!string.IsNullOrEmpty(searchFormModel.Mobile))
                {
                    queryable = queryable.Where(q => q.Mobile == searchFormModel.Mobile);
                }

                if (!string.IsNullOrEmpty(searchFormModel.NationalityCode))
                {
                    queryable = queryable.Where(q => q.NationalityCode == searchFormModel.NationalityCode);
                }

                if (searchFormModel.SortModel != null)
                    switch (searchFormModel.SortModel.Col + "|" + searchFormModel.SortModel.Sort)
                    {
                        case "firstname|asc":
                            queryable = queryable.OrderBy(x => x.FirstName);
                            break;
                        case "lastname|asc":
                            queryable = queryable.OrderBy(x => x.LastName);
                            break;
                        case "lastname|desc":
                            queryable = queryable.OrderByDescending(x => x.LastName);
                            break;
                        case "mobile|asc":
                            queryable = queryable.OrderBy(x => x.Mobile);
                            break;
                        case "mobile|desc":
                            queryable = queryable.OrderByDescending(x => x.Mobile);
                            break;
                        case "nationalityCode|asc":
                            queryable = queryable.OrderBy(x => x.NationalityCode);
                            break;
                        case "nationalityCode|desc":
                            queryable = queryable.OrderByDescending(x => x.NationalityCode);
                            break;
                        default:
                            queryable = queryable.OrderBy(x => x.NationalityCode);
                            break;
                    }
                else
                    queryable = queryable.OrderBy(x => x.NationalityCode);
                var resp = await queryable.Skip((searchFormModel.PageIndex - 1) * searchFormModel.PageSize).Take(searchFormModel.PageSize).ToListAsync();
                var count = await queryable.CountAsync();
                var searchViewModel = new SearchBookinationViewModel
                {
                    bookinationViewModels = resp.Select(respBook => new BookinationViewModel()
                    {
                        Id = respBook.Id,
                        FirstName = respBook.FirstName,
                        LastName = respBook.LastName,
                        NationalityCode = respBook.NationalityCode,
                        Description = respBook.Description,
                        Mobile = respBook.Mobile,
                        StartDate = respBook.StartDate,
                        EndDate = respBook.EndDate,
                        ReserveCode = respBook.ReserveCode
                    }).ToList(),
                    PageIndex = searchFormModel.PageIndex,
                    PageSize = searchFormModel.PageSize,
                    TotalCount = count
                };
                return searchViewModel;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(BookinationServiceErrors.GetError, ex);
            }
        }

        public async Task<bool> AddEntourageAsync(List<AddEntourageFormModel> addFormModel)
        {
            try
            {
                addFormModel.ForEach(async i =>
                {
                    var entourage = new BookinationEntourage()
                    {
                        Age = i.Age,
                        BookinationId = i.BookinationId,
                        GenderType = i.GenderType,
                        Name = i.Name,
                        NationalityCode = i.NationalityCode,
                        Relative = i.Relative
                    };

                    await _context.BookinationEntourages.AddAsync(entourage);
                });

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(BookinationServiceErrors.AddEntourageError, ex);
            }
        }

        public async Task<BookinationViewModel> GetByIdAsync(long id)
        {
            try
            {
                var bookination = await _context.Bookinations.FindAsync(id);
                if (bookination is null)
                    throw new ReservationGlobalException(BookinationServiceErrors.BookinationNotFoundError);

                return new BookinationViewModel()
                {
                    Description = bookination.Description,
                    EndDate = bookination.EndDate,
                    FirstName = bookination.FirstName,
                    Id = bookination.Id,
                    LastName = bookination.LastName,
                    Mobile = bookination.Mobile,
                    NationalityCode = bookination.NationalityCode,
                    ReserveCode = bookination.ReserveCode,
                    StartDate = bookination.StartDate
                };

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(BookinationServiceErrors.GetByIdError, ex);
            }
        }
    }
}
