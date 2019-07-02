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
    public class AccommodationUserAttachmentService:IAccommodationUserAttachmentService
    {
         private readonly ReservationDbContext _context;

        public AccommodationUserAttachmentService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<bool> AddReservationUserAttachmentRoomAsync(User user, List<AddReservationUserAttachmentFormModel> reservationForms)
        {
            try
            {
                var accRoomId = reservationForms.FirstOrDefault().AccommodationUserRoomId;
                var userAttachments = _context.AccommodationUserAttachments.Where(c => c.AccommodationUserRoomId == accRoomId).ToList();
                
                _context.AccommodationUserAttachments.RemoveRange(userAttachments);
                
                foreach (var reservationForm in reservationForms)
                {
                    AccommodationUserRoom accommodationUserRoom = null;
                    if (reservationForm.AccommodationUserRoomId != null)
                        accommodationUserRoom = _context.AccommodationUserRooms.FirstOrDefault(c => c.Id == reservationForm.AccommodationUserRoomId);
                    if (accommodationUserRoom is null)
                    {
                        throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.ReservationUserNotExistError);
                    }
                    var isDuplicateAccommodationUserAttachment = _context.AccommodationUserAttachments.Any(c => c.AccommodationUserRoomId == reservationForm.AccommodationUserRoomId && c.GenderType == reservationForm.GenderType && c.NationalCode == c.NationalCode);
                    if (!isDuplicateAccommodationUserAttachment)
                    {
                        var accommodationUserAt = new AccommodationUserAttachment()
                        {
                            AccommodationUserRoomId = accommodationUserRoom.Id,
                            Age = reservationForm.Age,
                            GenderType = reservationForm.GenderType,
                            Name = reservationForm.Name,
                            NationalCode = reservationForm.NationalCode,
                            Relative = reservationForm.Relative
                        };

                        await _context.AccommodationUserAttachments.AddAsync(accommodationUserAt);                      
                        
                    }
                    else
                    {
                        throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.AddDuplicateAccommodationUserAttachmentError);
                    }
                   
                   
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.AddAccommodationUserAttachmentError, ex);
            }
        }

        public async Task<bool> DeleteReservationUserAttachmentAsync(User user, List<long> reservationFormIds)
        {
            try
            {

                foreach (var acid in reservationFormIds)
                {
                    
                    var accommodationUserAt = await _context.AccommodationUserAttachments.FindAsync(acid);
                    _context.AccommodationUserAttachments.Remove(accommodationUserAt);
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.DeleteAccommodationUserAttachmentError, ex);
            }
        }

        public async Task<bool> EditReservationUserAttachmentRoomAsync(User user, EditReservationUserAttachmentFormModel reservationForm)
        {
            try
            {
                AccommodationUserAttachment accommodationUserAttachment = null;
                if (reservationForm.AccommodationUserAttachmentId != null)
                    accommodationUserAttachment = _context.AccommodationUserAttachments.FirstOrDefault(c => c.Id == reservationForm.AccommodationUserAttachmentId);
                if (accommodationUserAttachment is null)
                {
                    throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.EditAccommodationUserAttachmentNotExistError);
                }
                var isDuplicateAccommodationUserAttachment = _context.AccommodationUserAttachments.Any(c => c.AccommodationUserRoomId == reservationForm.AccommodationUserRoomId && c.GenderType == reservationForm.GenderType && c.NationalCode == c.NationalCode&& c.Id!=reservationForm.AccommodationUserAttachmentId);
                if (!isDuplicateAccommodationUserAttachment)
                {
                    accommodationUserAttachment.Name = reservationForm.Name;
                    accommodationUserAttachment.GenderType = reservationForm.GenderType;
                    accommodationUserAttachment.NationalCode = accommodationUserAttachment.NationalCode;
                    accommodationUserAttachment.Relative = accommodationUserAttachment.Relative;


                    _context.AccommodationUserAttachments.Update(accommodationUserAttachment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.EditDuplicateAccommodationUserAttachmentError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.EditAccommodationUserAttachmentError, ex);
            }
        }

        public async Task<SearchReservationUserAttachmentViewModel> GetReservationUserAttachmentAsync(User user, ReservationUserAttachmentSearchFormModel reservationForm)
        {
            try
            {

                var queryable = _context.AccommodationUserAttachments.Where(c => c.AccommodationUserRoomId == reservationForm.AccommodationUserId).AsQueryable();

               

                if (!string.IsNullOrEmpty(reservationForm.NationalCode))
                {
                    queryable = queryable.Where(q => q.NationalCode.Contains(reservationForm.NationalCode));
                }


                if (reservationForm.SortModel != null)
                    switch (reservationForm.SortModel.Col + "|" + reservationForm.SortModel.Sort)
                    {
                        
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
                var searchViewModel = new SearchReservationUserAttachmentViewModel();

                searchViewModel.searchReservationUserAttachmentViewModel = response.Select(selector: resp => new ReservationUserAttachmentViewModel()
                {
                    Id=resp.Id,
                    Age=resp.Age,
                    AccommodationUserRoomId=resp.AccommodationUserRoomId,
                    CreationDate=resp.CreationDate,
                    GenderType=resp.GenderType,
                    Name=resp.Name,
                    NationalCode=resp.NationalCode,
                    Relative=resp.Relative

                }).ToList();
                searchViewModel.PageIndex = reservationForm.PageIndex;
                searchViewModel.PageSize = reservationForm.PageSize;
                searchViewModel.TotalCount = count;
                return searchViewModel;
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(AccommodationUserAttachmentServiceErrors.GetAccommodationUserAttachmentError, ex);
            }
        }
    }
}