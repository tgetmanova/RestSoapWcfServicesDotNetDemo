using System;

using Serilog;
using Serilog.Core;

namespace UserRepositoryServiceApp.Logging
{
    /// <summary>
    /// Service logger.
    /// </summary>
    internal static class ServiceLogger
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Lazy<Logger> logger = new Lazy<Logger>(() => new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger());

        /// <summary>
        /// Gets the logger.
        /// </summary>
        internal static Logger Logger
        {
            get
            {
                return logger.Value;
            }
        }
    }
}
