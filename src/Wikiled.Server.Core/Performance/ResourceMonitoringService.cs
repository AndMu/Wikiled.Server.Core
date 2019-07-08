using Microsoft.Extensions.Hosting;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wikiled.Common.Utilities.Performance;

namespace Wikiled.Server.Core.Performance
{
    public class ResourceMonitoringService : IHostedService, IDisposable
    {
        private readonly ILogger<ResourceMonitoringService> logger;

        private IDisposable monitor;

        private readonly ISystemUsageCollector collector;

        private readonly IScheduler scheduler;

        private readonly TimeSpan scanTime = TimeSpan.FromMinutes(10);

        public ResourceMonitoringService(ILogger<ResourceMonitoringService> logger, IScheduler scheduler, IConfiguration config, ISystemUsageCollector collector)
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
            this.collector = collector ?? throw new ArgumentNullException(nameof(collector));
            this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
            logger.LogDebug("Will use scan every {0}", scanTime);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            monitor = Observable.Interval(scanTime, scheduler).StartWith(0).Subscribe(item => Monitor());
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            monitor?.Dispose();
            monitor = null;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            monitor?.Dispose();
            monitor = null;
        }

        private void Monitor()
        {
            collector.Refresh();
            logger.LogInformation("Service Monitoring. Working Set: {0:F2} Total CPU Used: {1::F2} User CPU: {2:F2}",
                                  collector.WorkingSet,
                                  collector.TotalCpuUsed,
                                  collector.UserCpuUsed);
        }
    }
}
