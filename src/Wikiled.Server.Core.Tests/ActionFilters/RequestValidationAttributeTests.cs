using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Wikiled.Server.Core.ActionFilters;
using Wikiled.Server.Core.Testing.Server;
using Wikiled.Server.Core.Tests.Helpers;

namespace Wikiled.Server.Core.Tests.ActionFilters
{
    [TestFixture]
    public class RequestValidationAttributeTests
    {
        private RequestValidationAttribute instance;

        private ContextManager<TestController> context;

        [SetUp]
        public void Setup()
        {
            context = new ContextManager<TestController>();
            instance = CreateRequestValidationAttribute();
        }
        
        [Test]
        public void OnActionExecutingError()
        {
            context.ModelStateDictionary.AddModelError("Error", "Error");
            instance.OnActionExecuting(context.ActionExecutedContext);
            Assert.IsInstanceOf<BadRequestObjectResult>(context.ActionExecutedContext.Result);
        }

        [Test]
        public void OnActionExecuting()
        {
            instance.OnActionExecuting(context.ActionExecutedContext);
            Assert.IsNull(context.ActionExecutedContext.Result);
        }

        private RequestValidationAttribute CreateRequestValidationAttribute()
        {
            return new RequestValidationAttribute();
        }
    }
}