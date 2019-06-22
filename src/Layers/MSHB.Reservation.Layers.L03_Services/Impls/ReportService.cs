using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MSHB.Reservation.Layers.L00_BaseModels.Cache;
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

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class ReportService: IReportService
    {
        private readonly ReservationDbContext _context;
        private IMemoryCache _cache;
        public ReportService(ReservationDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
            _cache = memoryCache;
        }

        public async Task<DashboardReportViewModel> GetDashboardReport()
        {
            try
            {
                var accommodationCount = string.Empty;
                var accommodationUserCount = string.Empty;
                var cityCount = string.Empty;
                var userCount = string.Empty;

                if (!_cache.TryGetValue(CacheKeys.AccommodationCount, out accommodationCount))
                {
                    MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                    cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
                    cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                    var accCount = await _context.AccommodationRooms.CountAsync();
                    accommodationCount = accCount.ToString();
                    _cache.Set<string>(CacheKeys.AccommodationCount, accCount.ToString(), cacheExpirationOptions);
    
                }
                if (!_cache.TryGetValue(CacheKeys.AccommodationUserCount, out accommodationUserCount))
                {
                    MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                    cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
                    cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                    var accUserCount = await _context.AccommodationUserRooms.CountAsync();
                    accommodationUserCount = accUserCount.ToString();
                    _cache.Set<string>(CacheKeys.AccommodationUserCount, accUserCount.ToString(), cacheExpirationOptions);

                }
                if (!_cache.TryGetValue(CacheKeys.CityCount, out cityCount))
                {
                    MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                    cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
                    cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                    var cCount = await _context.Citys.Where(c=>c.ParentId==null).CountAsync();
                    cityCount = cCount.ToString();
                    _cache.Set<string>(CacheKeys.CityCount, cCount.ToString(), cacheExpirationOptions);

                }
                if (!_cache.TryGetValue(CacheKeys.UserCount, out userCount))
                {
                    MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
                    cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(20);
                    cacheExpirationOptions.Priority = CacheItemPriority.Normal;
                    var uCount = await _context.Users.CountAsync();
                    userCount = uCount.ToString();
                    _cache.Set<string>(CacheKeys.UserCount, uCount.ToString(), cacheExpirationOptions);

                }
                return new DashboardReportViewModel()
                {
                    AccommodationUserCount = accommodationUserCount,
                    AccommodationCount = accommodationCount,
                    CityCount = cityCount,
                    UserCount = userCount
                };

            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(ReportServiceErrors.GetReportDashboard, ex);
            }
        }
    }
}
