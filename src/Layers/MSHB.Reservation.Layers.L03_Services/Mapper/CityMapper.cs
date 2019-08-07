using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Mapper
{
    public static class CityMapper
    {
        public static IQueryable<CityViewModel> Mapper(this IQueryable<City> model)
        {
            return model.Select(x => new CityViewModel()
            {
                Title = x.Title,
                Id = x.Id,
                ProvinceId = x.ProvinceId
            });
        }
    }
}
