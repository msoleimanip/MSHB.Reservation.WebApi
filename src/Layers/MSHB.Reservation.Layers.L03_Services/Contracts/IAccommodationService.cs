﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IAccommodationService
    {
        Task<bool> AddAsync(AddAccommodationFormModel accommodationForm);
        Task<SearchAccommodationViewModel> GetAsync(SearchAccommodationFormModel searchAccommodationForm);
        Task<bool> AddUnitAsync(AddUnitFormModel addUnitForm);
        Task<SearchAccommodationViewModel> SmartSearchAsync(SmartSearchFormModel smartSearchForm);
        Task<GetAccommodationUnitsViewModel> GetAccommodationUnitsAsync(AccommodationUnitFormModel accommodationUnitForm);
        Task<GetUnitViewModel> GetUnitAsync(long unitId);
        Task<List<AccommodationAttachmentViewModel>> GetAccommodationAttachmentsAsync(long AccommodationId);
        Task<bool> SetAccommodationAttachmentsAsync(AccommodationAttachmentsFormModel accommodationAttachmentsForm);
        Task<List<AccommodationAttachmentViewModel>> GetAttachmentsAsync(long accommodationId);
        Task<bool> DeleteAccommodationAttachmentAsync(DeleteAttachmentFormModel DeleteAttachmentForm);
    }
}
