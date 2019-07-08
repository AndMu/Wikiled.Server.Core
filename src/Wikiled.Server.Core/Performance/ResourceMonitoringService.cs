using Microsoft.Extensions.Hosting;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public ResourceMonitoringService(ILogger<ResourceMonitoringService> logger, ISystemUsageCollector collector, IScheduler scheduler)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.collector = collector ?? throw new ArgumentNullException(nameof(collector));
            this.scheduler = scheduler ?? throw new ArgumentNullException(nameof(scheduler));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            monitor = Observable.Interval(TimeSpan.FromMinutes(10), scheduler).StartWith(0).Subscribe(item => Monitor());
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
            logger.LogInformation("Service Monitoring. Working Set: {0} Total CPU Used: {1} User CPU: {2}",
                                  collector.WorkingSet,
                                  collector.TotalCpuUsed,
                                  collector.UserCpuUsed);
        }
    }
}
