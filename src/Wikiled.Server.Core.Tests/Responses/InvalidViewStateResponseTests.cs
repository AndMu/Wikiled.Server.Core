using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Wikiled.Core.Standard.Api.Server;
using Wikiled.Server.Core.Responses;

namespace Wikiled.Server.Core.Tests.Responses
{
    [TestFixture]
    public class InvalidViewStateResponseTests
    {
        private ModelStateDictionary modelstatedictionary;

        private InvalidViewStateResponse instance;

        [SetUp]
        public void Setup()
        {
            modelstatedictionary = new ModelStateDictionary();
            modelstatedictionary.AddModelError("Error", "Test Error");
            instance = CreateInvalidViewStateResponse();
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new InvalidViewStateResponse((ModelStateDictionary)null));
            Assert.Throws<ArgumentException>(() => new InvalidViewStateResponse(new ModelStateDictionary()));
            Assert.AreEqual(1, instance.Errors.Length);
            Assert.AreEqual(400, instance.Code);
            Assert.AreEqual("Serialization Error", instance.Status);
            Assert.AreEqual(ResponseType.Error, instance.ResponseType);
        }

        [Test]
        public void Serialization()
        {
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(instance);
            Assert.IsTrue(result.Contains("Test Error"));
        }

        private InvalidViewStateResponse CreateInvalidViewStateResponse()
        {
            return new InvalidViewStateResponse(modelstatedictionary);
        }
    }
}
