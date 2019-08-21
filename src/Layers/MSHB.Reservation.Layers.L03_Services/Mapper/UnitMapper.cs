using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Mapper
{
    public static class UnitMapper
    {
        public static List<AccommodationUnitViewModel> Mapper(this ICollection<Unit> model)
        {
            return model.Select(x => new AccommodationUnitViewModel()
            {
                UnitId = x.Id,
                AccommodationType = x.AccommodationType,
                DoubleBedCount = x.DoubleBedCount,
                IsActive = x.IsActive,
                MaximumCount = x.MaximumCount,
                MinimumCount = x.MinimumCount,
                RoomCount = x.RoomCount,
                SingleBedCount = x.SingleBedCount
            }).ToList();
        }

        public static AccommodationUnitViewModel Mapper(this Unit model)
        {
            return new AccommodationUnitViewModel()
            {
                UnitId = model.Id,
                AccommodationType = model.AccommodationType,
                DoubleBedCount = model.DoubleBedCount,
                IsActive = model.IsActive,
                MaximumCount = model.MaximumCount,
                MinimumCount = model.MinimumCount,
                RoomCount = model.RoomCount,
                SingleBedCount = model.SingleBedCount
            };
        }
    }
}
