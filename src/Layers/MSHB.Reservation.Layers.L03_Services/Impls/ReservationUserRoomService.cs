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
    public class ReservationUserRoomService: IReservationUserRoomService
    {
        private readonly ReservationDbContext _context;

        public ReservationUserRoomService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<long> AddReservationRoom(User user, AddReservationRoomFormModel reservationForm)
        {
            try
            {
                AccommodationRoom accommodationRoom = null;
                if (reservationForm.AccommodationRoomId != null)
                    accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == reservationForm.AccommodationRoomId);

                var isDuplicateAccommodation = _context.AccommodationUserRooms.Any(c => c.AccommodationRoomId == reservationForm.AccommodationRoomId &&
                                                    (((c.EntranceTime < reservationForm.EntranceTime) && (c.EndTime > reservationForm.EntranceTime))
                                                 || ((c.EntranceTime < reservationForm.EndTime) && (c.EndTime > reservationForm.EndTime))));
                if (!isDuplicateAccommodation)
                {
                    var accommodationUserRoom = new AccommodationUserRoom()
                    {
                        CreationDate = DateTime.Now,
                        AccommodationRoomId=accommodationRoom.Id,
                        GenderType= reservationForm.GenderType,
                        Description= reservationForm.Description,
                        EntranceTime= reservationForm.EntranceTime,
                        EndTime= reservationForm.EndTime,
                        NationalCode= reservationForm.NationalCode,
                        GuestCounts= reservationForm.GuestCounts,
                        PersonalCode= reservationForm.PersonalCode,
                        UserId=user.Id,
                        SystemCode=Guid.NewGuid(),
                        PriceAccommodation=  accommodationRoom.RoomPrice,
                        PhoneNumber= reservationForm.PhoneNumber
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

        public async  Task<bool> DeleteReservationRoom(User user, List<long> reservationFormIds)
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

        public async Task<bool> EditReservationRoom(User user, EditReservationRoomFormModel reservationForm)
        {
            try
            {
                AccommodationRoom accommodationRoom = null;
                if (reservationForm.AccommodationRoomId != null)
                    accommodationRoom = _context.AccommodationRooms.FirstOrDefault(c => c.Id == reservationForm.AccommodationRoomId);


                var accommodationUserRooms =await _context.AccommodationUserRooms.FindAsync( reservationForm.AccommodationUserRoomId);

                
                if (accommodationUserRooms != null)
                {
                    var isDuplicateAccommodation = _context.AccommodationUserRooms.Any(c =>c.Id!= reservationForm.AccommodationRoomId&& c.AccommodationRoomId == reservationForm.AccommodationRoomId &&
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

        public Task<SearchReservationRoomViewModel> GetUserReservationRoomForUserAsync(User user, ReservationRoomSearchFormModel reservationForm)
        {
            throw new NotImplementedException();
        }
    }
}
