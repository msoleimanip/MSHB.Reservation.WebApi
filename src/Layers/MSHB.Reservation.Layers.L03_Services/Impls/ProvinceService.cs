using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L03_Services.Mapper;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class ProvinceService : IProvinceService
    {
        private readonly ReservationDbContext _context;


        public ProvinceService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));

        }

        public async Task<List<ProvinceViewModel>> GetAsync()
        {
            try
            {
                var provinces = await _context.Provinces.Mapper().ToListAsync();

                return provinces;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ProvinceServiceErrors.GetError, ex);
            }
        }

        public async Task<ProvinceViewModel> GetAsync(long Id)
        {
            try
            {
                var province = await _context.Provinces.Mapper().FirstOrDefaultAsync();

                if (province is null)
                    throw new ReservationGlobalException(ProvinceServiceErrors.NotFoundIdError);

                return province;
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(ProvinceServiceErrors.GetSingleError, ex);
            }
        }
    }
}
