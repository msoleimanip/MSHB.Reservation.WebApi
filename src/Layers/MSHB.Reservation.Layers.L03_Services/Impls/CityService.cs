using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.Tree;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Layers.L02_DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MSHB.Reservation.Layers.L00_BaseModels.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MSHB.Reservation.Layers.L00_BaseModels.Settings;
using System.IO;
using System.Drawing;
using MSHB.Reservation.Layers.L03_Services.Mapper;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class CityService : ICityService
    {
        private readonly ReservationDbContext _context;


        public CityService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));

        }


        public async Task<List<CityViewModel>> GetAllAsync()
        {
            try
            {
                var provinces = await _context.Citys.Include(x=>x.Province).ToListAsync();
                var models = new List<CityViewModel>();
                provinces.ForEach(i =>
                {
                    models.Add(new CityViewModel()
                    {
                        Title =$"{i.Title} ({i.Province.Title})",
                        Id = i.Id,
                        ProvinceId = i.ProvinceId
                    });
                });

                return models;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.GetAllCitiesError, ex);
            }
        }

        public async Task<List<CityViewModel>> GetAsync(long ProvinceId)
        {
            try
            {
                var cities = await _context.Citys.Where(x => x.ProvinceId == ProvinceId).Mapper().ToListAsync();

                return cities;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.GetError, ex);
            }
        }

        public async Task<CityViewModel> GetAsync(long ProvinceId, long Id)
        {
            try
            {
                var city = await _context.Citys.Mapper().FirstOrDefaultAsync(x => x.Id == Id && x.ProvinceId == ProvinceId);

                if (city is null)
                    throw new ReservationGlobalException(CityServiceErrors.NotFoundIdError);

                return city;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.GetSingleError, ex);
            }
        }
    }
}
