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

        public async Task<long> AddAccommodationRoom(User user, AddAccommodationRoomFormModel accommodationForm)
        {
            try
            {
                City city = null;
                if (accommodationForm.CityId != null)
                    city = _context.Citys.FirstOrDefault(c => c.Id == accommodationForm.CityId);
                var isDuplicateAccommodation = _context.AccommodationRooms.Any(c => c.CityId == accommodationForm.CityId && c.RoomNumber == accommodationForm.RoomNumber.ToLower());
                if (!isDuplicateAccommodation)
                {
                    var accommodationRoom = new AccommodationRoom()
                    {
                        RoomNumber = accommodationForm.RoomNumber.ToLower(),
                        Bed = accommodationForm.Bed,
                        BedRoom = accommodationForm.BedRoom,
                        IsActivated = accommodationForm.IsActivated,
                        Description = accommodationForm.Description,
                        Rank = accommodationForm.Rank,
                        Capacity = accommodationForm.Capacity,
                        CityId = accommodationForm.CityId,
                        RoomPrice = accommodationForm.RoomPrice,
                       
                        RoomType = (RoomType)accommodationForm.RoomType,
                    };

                    await _context.AccommodationRooms.AddAsync(accommodationRoom);
                    await _context.SaveChangesAsync();
                    return accommodationRoom.Id;
                }
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.AddDuplicateAccommodationError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.AddAccommodationError, ex);
            }
        }

        public async Task<bool> DeactivateAccommodationRoomAsync(User user, DeactivateAccommodationRoomFormModel accommodationForm)
        {
            try
            {
                AccommodationRoom accommodationRoom = null;
                accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == accommodationForm.AccommodationRoomId);
                if (accommodationRoom != null)
                {

                    accommodationRoom.IsActivated = accommodationForm.IsActivated;
                    _context.AccommodationRooms.Update(accommodationRoom);
                    await _context.SaveChangesAsync();
                }
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.EditAccommodationNotExistError);
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(AccommodationRoomServiceErrors.ChangeStatusError, ex);
            }
        }

        public async Task<bool> DeleteAccommodationRoom(User user, List<long> accommodationFormIds)
        {
            try
            {

                foreach (var acid in accommodationFormIds)
                {
                    if (_context.AccommodationUserRooms.Any(c => c.AccommodationRoomId == acid))
                    {
                        throw new ReservationGlobalException(AccommodationRoomServiceErrors.UserInAccommodationExistError);
                    }
                    var accommodation = await _context.AccommodationRooms.FindAsync(acid);
                    _context.AccommodationRooms.Remove(accommodation);
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.DeleteAccommodationError, ex);
            }
        }

        public async Task<bool> EditAccommodationRoom(User user, EditAccommodationRoomFormModel accommodationForm)
        {

            try
            {
                AccommodationRoom accommodationRoom = null;
                accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == accommodationForm.AccommodationRoomId);
                if (accommodationRoom != null)
                {
                    var isDuplicateAccommodation = _context.AccommodationRooms.Any(c => c.RoomNumber == accommodationForm.RoomNumber.ToLower() && c.Id != accommodationForm.AccommodationRoomId);
                    if (!isDuplicateAccommodation)
                    {
                        accommodationRoom.RoomNumber = accommodationForm.RoomNumber.ToLower();
                        accommodationRoom.Bed = accommodationForm.Bed;
                        accommodationRoom.BedRoom = accommodationForm.BedRoom;
                        accommodationRoom.IsActivated = accommodationForm.IsActivated;
                        accommodationRoom.Description = accommodationForm.Description;
                        accommodationRoom.Rank = accommodationForm.Rank;
                        accommodationRoom.Capacity = accommodationForm.Capacity;
                        
                        accommodationRoom.RoomPrice = accommodationForm.RoomPrice;
                        accommodationRoom.RoomType = (RoomType)accommodationForm.RoomType;
                        _context.AccommodationRooms.Update(accommodationRoom);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    throw new ReservationGlobalException(AccommodationRoomServiceErrors.EditDuplicateAccommodationError);
                }
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.EditAccommodationNotExistError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.EditAccommodationError, ex);
            }
        }

        public async Task<SearchAccommodationRoomViewModel> GetUserAccommodationRoomForUserAsync(User user, AccommodationRoomSearchFormModel accommodationForm)
        {
            try
            {


                var queryable = _context.AccommodationRooms.Where(c=>c.CityId== accommodationForm.CityId).AsQueryable();
                if (accommodationForm.IsActivated.HasValue)
                    queryable = queryable.Where(q => q.IsActivated == accommodationForm.IsActivated);
                
                if (!string.IsNullOrEmpty(accommodationForm.RoomNumber))
                {
                    queryable = queryable.Where(q => q.RoomNumber == accommodationForm.RoomNumber);
                }
                if (accommodationForm.RoomType.HasValue)
                {
                    queryable = queryable.Where(q => q.RoomType == accommodationForm.RoomType);
                }
                if (accommodationForm.BedRoom.HasValue)
                {
                    queryable = queryable.Where(q => q.BedRoom == accommodationForm.BedRoom);
                }
                if (accommodationForm.Bed.HasValue)
                {
                    queryable = queryable.Where(q => q.Bed == accommodationForm.Bed);
                }
                if (accommodationForm.Capacity.HasValue)
                {
                    queryable = queryable.Where(q => q.Capacity >= accommodationForm.Capacity);
                }

                if (accommodationForm.StartTime.HasValue && accommodationForm.EndTime.HasValue)
                {
                    queryable = queryable.Where(q => !q.AccommodationUserRooms.All(c => ((c.EntranceTime < accommodationForm.StartTime)&& (c.EndTime > accommodationForm.StartTime))
                                                                                      || ((c.EntranceTime < accommodationForm.EndTime) && (c.EndTime > accommodationForm.EndTime))));
                }


                if (accommodationForm.SortModel != null)
                    switch (accommodationForm.SortModel.Col + "|" + accommodationForm.SortModel.Sort)
                    {
                        case "roomprice|asc":
                            queryable = queryable.OrderBy(x => x.RoomPrice);
                            break;
                        case "roomprice|desc":
                            queryable = queryable.OrderByDescending(x => x.RoomPrice);
                            break;
                      
                        case "isactivated|asc":
                            queryable = queryable.OrderBy(x => x.IsActivated);
                            break;
                        case "isactivated|desc":
                            queryable = queryable.OrderByDescending(x => x.IsActivated);
                            break;
                        default:
                            queryable = queryable.OrderBy(x => x.Rank);
                            break;
                    }
                else
                    queryable = queryable.OrderBy(x => x.Rank);
                var resp = await queryable.Skip((accommodationForm.PageIndex - 1) * accommodationForm.PageSize).Take(accommodationForm.PageSize).ToListAsync();
                var count = await queryable.CountAsync();
                var searchViewModel = new SearchAccommodationRoomViewModel();
                searchViewModel.searchAccommodationRoomViewModels = resp.Select( response => new AccommodationRoomViewModel()
                {
                    Id=response.Id,
                    AccommodationRoomId = response.Id,
                    RoomNumber = response.RoomNumber,
                    RoomPrice = response.RoomPrice,
                    BedRoom = response.BedRoom,
                    RoomType = response.RoomType,
                    Rank = response.Rank,
                    Bed = response.Bed,
                   
                    IsActivated = response.IsActivated,
                    Capacity = response.Capacity,
                    Description = response.Description,
                    CityId = response.CityId

                }).ToList();
                searchViewModel.PageIndex = accommodationForm.PageIndex;
                searchViewModel.PageSize = accommodationForm.PageSize;
                searchViewModel.TotalCount = count;
                return searchViewModel;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(AccommodationRoomServiceErrors.GetAccommodationError, ex);
            }
        }
    }
}
