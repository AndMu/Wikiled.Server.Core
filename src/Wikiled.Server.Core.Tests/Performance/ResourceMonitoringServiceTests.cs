using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wikiled.Common.Utilities.Performance;
using Wikiled.Server.Core.Performance;
using Wikiled.Server.Core.Testing.Helpers;

namespace Wikiled.Server.Core.Tests.Performance
{
    [TestFixture]
    public class ResourceMonitoringServiceTests
    {
        private Mock<ISystemUsageMonitor> mockSystemUsageCollector;

        private ResourceMonitoringService instance;

        private ConfigurationHelper config;

        [SetUp]
        public void SetUp()
        {
            mockSystemUsageCollector = new Mock<ISystemUsageMonitor>();
            config = new ConfigurationHelper();
            instance = CreateInstance();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new ResourceMonitoringService(null, config.Config.Object, mockSystemUsageCollector.Object));
            Assert.Throws<ArgumentNullException>(() => new ResourceMonitoringService(new NullLogger<ResourceMonitoringService>(), config.Config.Object, null));
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
            mockSystemUsageCollector.Verify(item => item.Dispose());
        }

        private ResourceMonitoringService CreateInstance()
        {
            return new ResourceMonitoringService(new NullLogger<ResourceMonitoringService>(), config.Config.Object, mockSystemUsageCollector.Object);
        }
    }
}
