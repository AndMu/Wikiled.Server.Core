using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Wikiled.Server.Core.Helpers;
using Wikiled.Server.Core.Testing.Controllers;
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
            Assert.Throws<ArgumentNullException>(() => new IpResolve((HttpContext)null));
            Assert.Throws<ArgumentNullException>(() => new IpResolve((IHttpContextAccessor)null));
            contextManager.HttpContextAccessor.Setup(item => item.HttpContext).Returns((HttpContext)null);
            var ipResolve = new IpResolve(contextManager.HttpContextAccessor.Object);
            var result = instance.GetRequestIp();
            Assert.AreEqual("Failed to resolve IP", result);
        }

        [TestCase("X-Forwarded-For", "127.0.0.1", "127.0.0.1")]
        [TestCase("X-Forwarded-For2", "127.0.0.1", "Failed to resolve IP")]
        [TestCase("REMOTE_ADDR", "127.0.0.1", "127.0.0.1")]
        public void GetRequestIp(string header, string value, string expected)
        {
            contextManager.RequestDictionary.Add(header, value);
            var result = instance.GetRequestIp();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetRequestIpConnection()
        {
            contextManager.ConnectionInfo.Setup(item => item.RemoteIpAddress).Returns(IPAddress.Parse("192.168.0.1"));
            var result = instance.GetRequestIp();
            Assert.AreEqual("192.168.0.1", result);
        }

        private IpResolve CreateIpResolve()
        {
            return new IpResolve(contextManager.HttpContextAccessor.Object);
        }
    }
}
