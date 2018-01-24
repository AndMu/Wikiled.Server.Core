using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
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
            Assert.Throws<ArgumentNullException>(() => new InvalidViewStateResponse(null));
            ValidationResult model = (ValidationResult)instance.Value;
            Assert.AreEqual(1, model.Errors.Length);
            Assert.AreEqual(422, instance.StatusCode);
            Assert.AreEqual("Validation Failed", model.Message);
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
