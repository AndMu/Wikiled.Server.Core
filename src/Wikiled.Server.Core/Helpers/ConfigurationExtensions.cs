using System;
using Microsoft.Extensions.Configuration;
using NLog;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Wikiled.Server.Core.Helpers
{
    public static class ConfigurationExtension
    {
        public static void ChangeNlog(this IConfiguration configuration, string directory = "logDirectory", string path = "path")
        {
            // manually refresh of NLog configuration
            // as it is not picking up global
            try
            {
                if (LogManager.Configuration == null)
                {
                    throw new NLogConfigurationException("LogManager.Configuration is null");
                }

                if (!LogManager.Configuration.Variables.ContainsKey(directory))
                {
                    throw new NLogConfigurationException($"{directory} is not setup as variable");
                }

                var configurationPath = configuration.GetSection("logging");
                if (configurationPath == null)
                {
                    throw new NLogConfigurationException("configurationPath is not setup");
                }

                LogManager.Configuration.Variables[directory] = configurationPath.GetValue<string>(path);

                var logLevel = configuration.GetValue<LogLevel>("Logging:LogLevel:Default");
                switch (logLevel)
                {
                    case LogLevel.Trace:
                        LogManager.GlobalThreshold = NLog.LogLevel.Trace;
                        break;
                    case LogLevel.Debug:
                        LogManager.GlobalThreshold = NLog.LogLevel.Debug;
                        break;
                    case LogLevel.Information:
                        LogManager.GlobalThreshold = NLog.LogLevel.Info;
                        break;
                    case LogLevel.Warning:
                        LogManager.GlobalThreshold = NLog.LogLevel.Warn;
                        break;
                    case LogLevel.Error:
                        LogManager.GlobalThreshold = NLog.LogLevel.Error;
                        break;
                    case LogLevel.Critical:
                        LogManager.GlobalThreshold = NLog.LogLevel.Fatal;
                        break;
                    case LogLevel.None:
                        LogManager.GlobalThreshold = NLog.LogLevel.Off;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (NLogConfigurationException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NLogConfigurationException("Failed to setup NLog", e);
            }
        }
    }
}