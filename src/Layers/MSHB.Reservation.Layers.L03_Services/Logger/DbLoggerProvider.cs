using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using MSHB.Reservation.Shared.Common.GuardToolkit;

namespace MSHB.Reservation.Layers.L03.Services.Logger
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceScopeFactory _scopeFactory;

        public DbLoggerProvider(
                    Func<string, LogLevel, bool> filter,
                    IServiceScopeFactory scopeFactory,
        IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceProvider.CheckArgumentIsNull(nameof(_serviceProvider));

            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _filter = filter;
            _filter.CheckArgumentIsNull(nameof(_filter));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_serviceProvider, _scopeFactory, categoryName, _filter);
        }

        public void Dispose()
        {
        }
    }
}
