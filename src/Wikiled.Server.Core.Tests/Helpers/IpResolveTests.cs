using System;
using System.Net;
using NUnit.Framework;
using Wikiled.Server.Core.Helpers;
using Wikiled.Server.Core.Testing.Server;

namespace Wikiled.Server.Core.Tests.Helpers
{
    [TestFixture]
    public class IpResolveTests
    {
        private ContextManager<TestController> contextManager;

        private IpResolve instance;

        [SetUp]
        public void Setup()
        {
            contextManager = new ContextManager<TestController>();
            instance = CreateIpResolve();
        }

        [Test]
        public void GetRequestIpNull()
        {
            var ipResolve = new IpResolve(null);
            Assert.Throws<Exception>(() => ipResolve.GetRequestIp());
        }

        [TestCase("X-Forwarded-For", "127.0.0.1", "127.0.0.1")]
        [TestCase("X-Forwarded-For2", "127.0.0.1", null)]
        [TestCase("REMOTE_ADDR", "127.0.0.1", "127.0.0.1")]
        public void GetRequestIp(string header, string value, string expected)
        {
            contextManager.RequestDictionary.Add(header, value);
            if (!string.IsNullOrEmpty(expected))
            {
                var result = instance.GetRequestIp();
                Assert.AreEqual(expected, result);
            }
            else
            {
                Assert.Throws<Exception>(() => instance.GetRequestIp());
            }
        }

        [Test]
        public void GetRequestIpConnection()
        {
            contextManager.ConnectionInfo.RemoteIpAddress = IPAddress.Parse("192.168.0.1");
            var result = instance.GetRequestIp();
            Assert.AreEqual("192.168.0.1", result);
        }

        private IpResolve CreateIpResolve()
        {
            return new IpResolve(contextManager.HttpContextAccessor.Object);
        }
    }
}
