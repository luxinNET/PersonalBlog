using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalBlogweb.Common
{

    public class LogHelper
    {
        private static ILoggerFactory _loggerFactory;
        public static ILoggerFactory LoggerFactory
        {
            set
            {
                if (_loggerFactory == null)
                {
                    _loggerFactory = value;
                }
            }
        }

        public static ILogger<T> CreateLogger<T>()
        {
            ILogger<T> logger = _loggerFactory.CreateLogger<T>();
            return logger;
        }

        public static ILogger CreateLogger(string loggerName)
        {
            ILogger logger = _loggerFactory.CreateLogger(loggerName);
            return logger;
        }
    }
}
