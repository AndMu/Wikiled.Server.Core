using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Wikiled.Server.Core.Responses;

namespace Wikiled.Server.Core.Tests.Responses
{
    [TestFixture]
    public class ValidationResultTests
    {
        private ModelStateDictionary stateDictionary;

        private ValidationResult instance;

        [SetUp]
        public void SetUp()
        {
            stateDictionary = new ModelStateDictionary();
            stateDictionary.AddModelError("Error", "Test Error");
            instance = CreateValidationResult();
        }

        [Test]
        public void String()
        {
            var result = instance.ToString();
            Assert.AreEqual("Serialization Error: [Error]: <Test Error>;", result);
        }

        [Test]
        public void Construct()
        {
            Assert.Throws<ArgumentNullException>(() => new ValidationResult(null));
        }

        private ValidationResult CreateValidationResult()
        {
            return new ValidationResult(stateDictionary);
        }
    }
}