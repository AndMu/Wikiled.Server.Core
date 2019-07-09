using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wikiled.Common.Utilities.Performance;

namespace Wikiled.Server.Core.Performance
{
    public class ResourceMonitoringService : IHostedService, IDisposable
    {
        private readonly ILogger<ResourceMonitoringService> logger;

        private ISystemUsageMonitor monitor;

        private readonly TimeSpan scanTime = TimeSpan.FromMinutes(10);

        public ResourceMonitoringService(ILogger<ResourceMonitoringService> logger, IConfiguration config, ISystemUsageMonitor monitor)
        {
            var performance = config.GetSection("performance");
            if (performance != null)
            {
                var scan = performance.GetValue<int>("scan");
                if (scan > 0)
                {
                    scanTime = TimeSpan.FromMinutes(scan);
                }
            }

            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.monitor = monitor ?? throw new ArgumentNullException(nameof(monitor));
            logger.LogDebug("Will scan every {0}", scanTime);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            monitor.Refreshed += MonitorOnRefreshed;
            monitor.Start(scanTime, TimeSpan.FromMilliseconds(scanTime.TotalMilliseconds * 3));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            StopProcessing();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            StopProcessing();
        }

        private void StopProcessing()
        {
            if (monitor == null)
            {
                return;
            }

            monitor.Refreshed -= MonitorOnRefreshed;
            monitor.Dispose();
            monitor = null;
        }

        private void MonitorOnRefreshed(object sender, EventArgs e)
        {
            logger.LogInformation("MAX: {0}", monitor.UsageBucket.Max.GetBasic());
            logger.LogInformation("Average: {0}", monitor.UsageBucket.Average.GetBasic());
        }
    }
}
