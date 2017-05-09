using System;

using Serilog;
using Serilog.Core;

namespace UserRepositoryServiceApp.Logging
{
    static class ServiceLogger
    {
        private static Lazy<Logger> logger = new Lazy<Logger>(() => new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger());

        public static Logger Logger
        {
            get
            {
                return logger.Value;
            }
        }
    }
}
