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
    public class ReservationUserRoomService : IReservationUserRoomService
    {
        private readonly ReservationDbContext _context;

        public ReservationUserRoomService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<long> AddReservationRoomAsync(User user, AddReservationRoomFormModel reservationForm)
        {
            try
            {
                AccommodationRoom accommodationRoom = null;
                if (reservationForm.AccommodationRoomId != null)
                    accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == reservationForm.AccommodationRoomId);

                var isDuplicateAccommodation = _context.AccommodationUserRooms.Any(c => c.AccommodationRoomId == reservationForm.AccommodationRoomId &&
                                                    (((c.EntranceTime <= reservationForm.EntranceTime) && (c.EndTime > reservationForm.EntranceTime))
                                                 || ((c.EntranceTime < reservationForm.EndTime) && (c.EndTime >= reservationForm.EndTime))));
                if (!isDuplicateAccommodation)
                {
                    var accommodationUserRoom = new AccommodationUserRoom()
                    {
                        CreationDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        AccommodationRoomId = accommodationRoom.Id,
                        GenderType = reservationForm.GenderType,                       
                        Description = reservationForm.Description,                   
                        EntranceTime = reservationForm.EntranceTime,
                        EndTime = reservationForm.EndTime,
                        NationalCode = reservationForm.NationalCode,
                        GuestCounts = reservationForm.GuestCounts,
                        PersonalCode = reservationForm.PersonalCode,
                        UserId = user.Id,
                        PriceAccommodation = accommodationRoom.RoomPrice,
                        PhoneNumber = reservationForm.PhoneNumber,
                        CityId= (long)accommodationRoom.CityId,
                        Status=StatusReservationType.Registered
                       

                    };

                    await _context.AccommodationUserRooms.AddAsync(accommodationUserRoom);
                    await _context.SaveChangesAsync();
                    return accommodationUserRoom.Id;
                }
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.AddDuplicateReservationError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.AddReservationError, ex);
            }
        }

        public async Task<bool> DeleteReservationRoomAsync(User user, List<long> reservationFormIds)
        {
            try
            {

                foreach (var acid in reservationFormIds)
                {

                    var accommodationUser = await _context.AccommodationUserRooms.FindAsync(acid);
                    _context.AccommodationUserRooms.Remove(accommodationUser);
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationRoomServiceErrors.DeleteAccommodationError, ex);
            }
        }

        public async Task<bool> EditReservationRoomAsync(User user, EditReservationRoomFormModel reservationForm)
        {
            try
            {
                AccommodationRoom accommodationRoom = null;
                if (reservationForm.AccommodationRoomId != null)
                    accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == reservationForm.AccommodationRoomId);


                var accommodationUserRooms = await _context.AccommodationUserRooms.FindAsync(reservationForm.AccommodationUserRoomId);


                if (accommodationUserRooms != null)
                {
                    var isDuplicateAccommodation = _context.AccommodationUserRooms.Any(c => c.Id != reservationForm.AccommodationRoomId && c.AccommodationRoomId == reservationForm.AccommodationRoomId &&
                                                    (((c.EntranceTime < reservationForm.EntranceTime) && (c.EndTime > reservationForm.EntranceTime))
                                                 || ((c.EntranceTime < reservationForm.EndTime) && (c.EndTime > reservationForm.EndTime))));
                    if (!isDuplicateAccommodation)
                    {

                        accommodationUserRooms.LastUpdateDate = DateTime.Now;
                        accommodationUserRooms.AccommodationRoomId = accommodationRoom.Id;
                        accommodationUserRooms.GenderType = reservationForm.GenderType;
                        accommodationUserRooms.Description = reservationForm.Description; 
                        accommodationUserRooms.EntranceTime = reservationForm.EntranceTime;
                        accommodationUserRooms.EndTime = reservationForm.EndTime;
                        accommodationUserRooms.NationalCode = reservationForm.NationalCode;
                        accommodationUserRooms.GuestCounts = reservationForm.GuestCounts;
                        accommodationUserRooms.PersonalCode = reservationForm.PersonalCode;
                        accommodationUserRooms.UserId = user.Id;
                        accommodationUserRooms.PriceAccommodation = accommodationRoom.RoomPrice;
                        accommodationUserRooms.PhoneNumber = reservationForm.PhoneNumber;
                        
                        


                        _context.AccommodationUserRooms.Update(accommodationUserRooms);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    throw new ReservationGlobalException(ReservationUserRoomServiceErrors.EditDuplicateReservationError);
                }
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.EditReservationNotExistError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.EditReservationError, ex);
            }
        }

        public async Task<SearchReservationRoomViewModel> GetUserReservationRoomForUserAsync(User user, ReservationRoomSearchFormModel reservationForm)
        {
            try
            {

                var queryable = _context.AccommodationUserRooms.Include(c => c.AccommodationRoom).Include(d => d.User).Where(c => c.CityId == reservationForm.CityId).AsQueryable();

                if (reservationForm.AccommodationUserRoomId != null && reservationForm.AccommodationUserRoomId.Count > 0)
                {
                    queryable = queryable.Where(q => reservationForm.AccommodationUserRoomId.Contains(q.Id));
                }

                if (!string.IsNullOrEmpty(reservationForm.NationalCode))
                {
                    queryable = queryable.Where(q => q.NationalCode.Contains(reservationForm.NationalCode));
                }

                if (!string.IsNullOrEmpty(reservationForm.PhoneNumber))
                {
                    queryable = queryable.Where(q => q.PhoneNumber.Contains(reservationForm.PhoneNumber));
                }
                if (!string.IsNullOrEmpty(reservationForm.PersonalCode))
                {
                    queryable = queryable.Where(q => q.PersonalCode.Contains(reservationForm.PersonalCode));
                }
                if (reservationForm.CreationDate.HasValue)
                {
                    queryable = queryable.Where(q => q.CreationDate >= reservationForm.CreationDate.Value);
                }
                if (reservationForm.EntranceTime.HasValue)
                {
                    queryable = queryable.Where(q => q.EntranceTime >= reservationForm.EntranceTime.Value);
                }
                if (reservationForm.EndTime.HasValue)
                {
                    queryable = queryable.Where(q => q.EndTime >= reservationForm.EndTime.Value);
                }
                if (reservationForm.SystemCode.HasValue)
                {
                    queryable = queryable.Where(q => q.SystemCode.ToString().Contains(reservationForm.SystemCode.HasValue.ToString()));
                }


                if (reservationForm.SortModel != null)
                    switch (reservationForm.SortModel.Col + "|" + reservationForm.SortModel.Sort)
                    {
                        case "entrancetime|asc":
                            queryable = queryable.OrderBy(x => x.EntranceTime);
                            break;
                        case "entrancetime|desc":
                            queryable = queryable.OrderByDescending(x => x.EntranceTime);
                            break;
                        case "endtime|asc":
                            queryable = queryable.OrderBy(x => x.EndTime);
                            break;
                        case "endtime|desc":
                            queryable = queryable.OrderByDescending(x => x.EndTime);
                            break;
                        case "lastupdatedate|asc":
                            queryable = queryable.OrderBy(x => x.LastUpdateDate);
                            break;
                        case "lastupdatedate|desc":
                            queryable = queryable.OrderByDescending(x => x.LastUpdateDate);
                            break;
                        case "creationdate|asc":
                            queryable = queryable.OrderBy(x => x.CreationDate);
                            break;
                        case "creationdate|desc":
                            queryable = queryable.OrderByDescending(x => x.CreationDate);
                            break;
                        default:
                            queryable = queryable.OrderBy(x => x.CreationDate);
                            break;
                    }
                else
                    queryable = queryable.OrderBy(x => x.CreationDate);
                var response = await queryable.Skip((reservationForm.PageIndex - 1) * reservationForm.PageSize).Take(reservationForm.PageSize).ToListAsync();
                var count = await queryable.CountAsync();
                var searchViewModel = new SearchReservationRoomViewModel();

                searchViewModel.searchReservationRoomViewModels = response.Select(selector: resp => new ReservationRoomViewModel()
                {
                    Id = resp.Id,
                    AccommodationRoomId = resp.AccommodationRoomId,
                    Bed = resp.AccommodationRoom.Bed,
                    BedRoom = resp.AccommodationRoom.BedRoom,
                    CityId = resp.CityId,
                    Capacity = resp.AccommodationRoom.Capacity,
                    CreationDate = resp.CreationDate,
                    Description = resp.Description,
                    EndTime = resp.EndTime,
                    EntranceTime = resp.EntranceTime,
                    GenderType = resp.GenderType,
                    GuestCounts = resp.GuestCounts,
                    IsActivated = resp.AccommodationRoom.IsActivated,
                    LastUpdateDate = resp.LastUpdateDate,
                    NationalCode = resp.NationalCode,
                    PersonalCode = resp.PersonalCode,
                    PhoneNumber = resp.PhoneNumber,
                    Rank = resp.AccommodationRoom.Rank,
                    RoomNumber = resp.AccommodationRoom.RoomNumber,
                    RoomPrice = resp.AccommodationRoom.RoomPrice,
                    RoomType = resp.AccommodationRoom.RoomType,
                    PriceAccommodation = resp.PriceAccommodation,
                    SystemCode = resp.SystemCode,
                    UsernameAssignment = resp.User.Username,
                    Status=resp.Status

                }).ToList();
                searchViewModel.PageIndex = reservationForm.PageIndex;
                searchViewModel.PageSize = reservationForm.PageSize;
                searchViewModel.TotalCount = count;
                return searchViewModel;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(UsersServiceErrors.GetUserListError, ex);
            }

        }
    }
}
