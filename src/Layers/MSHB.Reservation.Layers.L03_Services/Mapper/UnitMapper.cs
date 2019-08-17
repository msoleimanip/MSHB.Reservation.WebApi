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
        public static IQueryable<AccommodationUnitViewModel> Mapper(this IQueryable<Unit> model)
        {
            return model.Select(x => new AccommodationUnitViewModel()
            {
                AccommodationType = x.AccommodationType,
                DoubleBedCount = x.DoubleBedCount,
                IsActive = x.IsActive,
                MaximumCount = x.MaximumCount,
                MinimumCount = x.MinimumCount,
                RoomCount = x.RoomCount,
                SingleBedCount = x.SingleBedCount
            });
        }
    }
}
