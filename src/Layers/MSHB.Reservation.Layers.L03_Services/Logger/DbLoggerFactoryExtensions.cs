using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace MSHB.Reservation.Layers.L03.Services.Logger
{
    public static class DbLoggerFactoryExtensions
    {
        public static ILoggerFactory AddDbLogger(
                    this ILoggerFactory factory,
                    IServiceProvider serviceProvider,
                    IServiceScopeFactory scopeFactory,
                    LogLevel minLevel)
        {
            Func<string, LogLevel, bool> logFilter = delegate (string loggerName, LogLevel logLevel)
            {
                return logLevel >= minLevel;
            };

            factory.AddProvider(new DbLoggerProvider(logFilter, scopeFactory, serviceProvider));
            return factory;
        }
    }
}
