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

        public Task<long> AddReservationUserAttachmentRoomAsync(User user, AddReservationUserAttachmentFormModel reservationForm)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReservationUserAttachmentAsync(User user, List<long> reservationFormIds)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditReservationUserAttachmentRoomAsync(User user, EditReservationUserAttachmentFormModel reservationForm)
        {
            throw new NotImplementedException();
        }

        public Task<SearchReservationUserAttachmentViewModel> GetReservationUserAttachmentAsync(User user, ReservationUserAttachmentSearchFormModel reservationForm)
        {
            throw new NotImplementedException();
        }
    }
}