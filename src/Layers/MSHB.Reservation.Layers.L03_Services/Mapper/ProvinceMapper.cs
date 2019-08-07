using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSHB.Reservation.Layers.L03_Services.Mapper
{
    public static class ProvinceMapper
    {
        public static IQueryable<ProvinceViewModel> Mapper(this IQueryable<Province> model)
        {
            return model.Select(x => new ProvinceViewModel()
            {
                Title = x.Title,
                Id = x.Id
            });
        }
    }
}
