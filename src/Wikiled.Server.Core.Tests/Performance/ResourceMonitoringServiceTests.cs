using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Reactive.Testing;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wikiled.Common.Utilities.Performance;
using Wikiled.Server.Core.Performance;

namespace Wikiled.Server.Core.Tests.Performance
{
    [TestFixture]
    public class ResourceMonitoringServiceTests
    {
        private Mock<ISystemUsageCollector> mockSystemUsageCollector;

        private TestScheduler scheduler;

        private ResourceMonitoringService instance;

        [SetUp]
        public void SetUp()
        {
            mockSystemUsageCollector = new Mock<ISystemUsageCollector>();
            scheduler = new TestScheduler();
            instance = CreateInstance();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new ResourceMonitoringService(null, mockSystemUsageCollector.Object, scheduler));
            Assert.Throws<ArgumentNullException>(() => new ResourceMonitoringService(new NullLogger<ResourceMonitoringService>(), null, scheduler));
            Assert.Throws<ArgumentNullException>(() => new ResourceMonitoringService(new NullLogger<ResourceMonitoringService>(), mockSystemUsageCollector.Object, null));
        }

        [Test]
        public void DisposeNotInitialized()
        {
            instance.Dispose();
        }

        [Test]
        public async Task DisposeInitialized()
        {
            await instance.StartAsync(CancellationToken.None).ConfigureAwait(false);
            instance.Dispose();
        }

        [Test]
        public async Task Monitor()
        {
            await instance.StartAsync(CancellationToken.None).ConfigureAwait(false);
            mockSystemUsageCollector.Verify(item => item.Refresh(), Times.Exactly(1));
            scheduler.AdvanceBy(TimeSpan.FromMinutes(10).Ticks);
            mockSystemUsageCollector.Verify(item => item.Refresh(), Times.Exactly(2));
        }

        private ResourceMonitoringService CreateInstance()
        {
            return new ResourceMonitoringService(new NullLogger<ResourceMonitoringService>(),  mockSystemUsageCollector.Object, scheduler);
        }
    }
}
