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
                                                 || ((c.EntranceTime < reservationForm.EndTime) && (c.EndTime >= reservationForm.EndTime))) && (c.Status != StatusReservationType.Cancel && c.Status != StatusReservationType.CheckOut));
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
                        IsPaied = false,
                        PriceAccommodation = accommodationRoom.RoomPrice,
                        PhoneNumber = reservationForm.PhoneNumber,
                        CityId = (long)accommodationRoom.CityId,
                        Status = StatusReservationType.Registered


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

        public async  Task<bool> ChangeStatusReservationRoomAsync(User user, ChangeStatusReservationRoom changeStatusReservationRoom)
        {
            try
            {
                var accommodationUserRooms = await _context.AccommodationUserRooms.FindAsync(changeStatusReservationRoom.AccommodationUserRoomId);
                if (accommodationUserRooms!=null)
                {
                    if (accommodationUserRooms.Status==StatusReservationType.Cancel|| accommodationUserRooms.Status == StatusReservationType.CheckOut)
                    {
                        throw new ReservationGlobalException(ReservationUserRoomServiceErrors.StatusReservationTypeError);
                    }
                    accommodationUserRooms.Status = changeStatusReservationRoom.Status;
                   await _context.SaveChangesAsync();
                    return true;
                }
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.EditReservationNotExistError);

            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(AccommodationRoomServiceErrors.ChangeStatusError, ex);
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
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.DeleteReservationError, ex);
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
                                                 || ((c.EntranceTime < reservationForm.EndTime) && (c.EndTime > reservationForm.EndTime))) && (c.Status != StatusReservationType.Cancel && c.Status != StatusReservationType.CheckOut));
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

        public async Task<List<ReservationFailViewModel>> GetReservationFailsAsync()
        {
            try
            {
                var accommodationUserRooms = await _context.AccommodationUserRooms.Where(c=> c.EntranceTime>=DateTime.Now && c.Status==StatusReservationType.Registered).ToListAsync();
                var reservationFails = new List<ReservationFailViewModel>();
                accommodationUserRooms.ForEach(au =>
                {
                    if (string.IsNullOrEmpty(au.PhoneNumber) && au.PhoneNumber.Length>3)
                    {
                        var number = au.PhoneNumber;
                        if (au.PhoneNumber.StartsWith("98"))
                        {
                            number = au.PhoneNumber.Substring(2);
                        }
                        else if (au.PhoneNumber.StartsWith("0"))
                        {
                            number= au.PhoneNumber.Substring(1);
                        }
                        long numberPhone;
                        if (long.TryParse(number, out numberPhone))
                            reservationFails.Add(new ReservationFailViewModel()
                            {
                                Number = numberPhone,
                                Content = "کاربر گرامی با توجه به عدم حضور جناب عالی تا کنون در اقامتگاه جهت کنسل کردن اقامتگاه کد پیگیری زیر را ارسال نمایید."
                                + "\n" + au.SystemCode.ToString()
                         });
                    }
                    

                });
                return reservationFails;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.GetReservationFailsError,ex);
            }
        }

        public async Task<ReservationRoomViewModel> GetUserReservationRoomByIdAsync(User user, long id)
        {
            try
            {

                var resp = await _context.AccommodationUserRooms.Include(c => c.AccommodationRoom).Include(d => d.User).Include(x => x.City).FirstOrDefaultAsync(c => c.Id == id);

                var result = new ReservationRoomViewModel()
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
                    Status = resp.Status,
                    FileId = resp.City.FileId,
                    IsPaied = resp.IsPaied,
                    PaymentType = resp.PaymentType,
                    TotalRoomPrice = resp.EndTime.HasValue ? (long)(resp.AccommodationRoom.RoomPrice * (resp.EndTime.Value.Subtract(resp.EntranceTime.Value).TotalDays)) : resp.AccommodationRoom.RoomPrice,

                };
               
                return result;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.GetReservationError, ex);
            }

        }

        public async Task<SearchReservationRoomViewModel> GetUserReservationRoomForUserAsync(User user, ReservationRoomSearchFormModel reservationForm)
        {
            try
            {

                var queryable = _context.AccommodationUserRooms.Include(c => c.AccommodationRoom).Include(d => d.User).Include(x => x.City).Where(c => c.CityId == reservationForm.CityId).AsQueryable();

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
                    Status = resp.Status,
                    FileId = resp.City.FileId,
                    IsPaied=resp.IsPaied,
                    PaymentType=resp.PaymentType,
                    TotalRoomPrice = resp.EndTime.HasValue ? (long)(resp.AccommodationRoom.RoomPrice * (resp.EndTime.Value.Subtract(resp.EntranceTime.Value).TotalDays)) : resp.AccommodationRoom.RoomPrice,

                }).ToList();
                searchViewModel.PageIndex = reservationForm.PageIndex;
                searchViewModel.PageSize = reservationForm.PageSize;
                searchViewModel.TotalCount = count;
                return searchViewModel;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.GetReservationError, ex);
            }

        }

        public async Task<bool> ReceiveContentAsync(MessageContent messageContent)
        {
            try
            {
                if (!string.IsNullOrEmpty(messageContent.Content) && messageContent.Number!=0)
                {       
                    long systemCode;          
                       if (long.TryParse(messageContent.Content,out systemCode))
                       {
                            var accommodationUserRooms = await _context.AccommodationUserRooms.FirstOrDefaultAsync(c=>c.SystemCode==systemCode);
                            if (accommodationUserRooms != null)
                            {
                                var phonenumber=messageContent.Number.ToString();
                                var phonenumber2=messageContent.Number.ToString();
                                var phonenumber3=messageContent.Number.ToString();

                                if (phonenumber.StartsWith("989"))
                                {
                                    phonenumber2="0"+phonenumber.Substring(2);
                                    phonenumber3=phonenumber.Substring(2);
                                }
                                   
                                else if (phonenumber.StartsWith("0"))
                                {
                                    phonenumber2="98"+phonenumber.Substring(1);
                                    phonenumber3=phonenumber.Substring(1);
                                }
                                   
                                else if (!phonenumber.StartsWith("0") && !phonenumber.StartsWith("98") && phonenumber.Length==10)
                                {
                                    phonenumber2="0"+phonenumber;
                                    phonenumber3="98"+phonenumber;
                                }
                                   
                                if (accommodationUserRooms.PhoneNumber==phonenumber||accommodationUserRooms.PhoneNumber==phonenumber2||accommodationUserRooms.PhoneNumber==phonenumber3)
                                {
                                    accommodationUserRooms.Status = StatusReservationType.Cancel;
                                    _context.AccommodationUserRooms.Update(accommodationUserRooms);
                                    await _context.SaveChangesAsync();
                                }

                                
                            }                              
                       }                                     

                }
                return true;              

            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(AccommodationRoomServiceErrors.ChangeStatusError, ex);
            }

        }

        public async Task<bool> UserReservationRoomPaiedAsync(User user, ReservationRoomPaiedFormModel reservationForm)
        {
            try
            {
                var accommodationUserRooms = await _context.AccommodationUserRooms.FindAsync(reservationForm.ReservationUserRoomId);


                if (accommodationUserRooms != null)
                {
                    accommodationUserRooms.IsPaied = reservationForm.IsPaied;
                    accommodationUserRooms.PaymentType = reservationForm.PaymentType;
                    accommodationUserRooms.PriceAccommodation = reservationForm.PriceAccommodation;
                    accommodationUserRooms.Description = reservationForm.Description;
                    _context.AccommodationUserRooms.Update(accommodationUserRooms);
                    await _context.SaveChangesAsync();
                    return true;

                }
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.EditReservationNotExistError);
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ReservationUserRoomServiceErrors.UserReservationRoomPaied);

            }
        }
    }
}
