using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Mapper
{
    public static class AccommodationMapper
    {
        public static AccommodationViewModel Mapper(this Accommodation model)
        {
            return new AccommodationViewModel()
            {
                Address = model.Address,
                Caption = model.Caption,
                Id = model.Id,
                CityTitle = model.City.Title,
                Code = model.Code,
                District = model.District,
                FileId = model.FileId,
                IsActivated = model.IsActivated,
                Number = model.Number
            };
        }
    }
}
