using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using Wikiled.Server.Core.ActionFilters;
using Wikiled.Server.Core.Responses;
using Wikiled.Server.Core.Testing.Controllers;
using Wikiled.Server.Core.Testing.Server;

namespace Wikiled.Server.Core.Tests.ActionFilters
{
    [TestFixture]
    public class RequestValidationAttributeTests
    {
        private RequestValidationAttribute instance;

        private ContextManager<TestController> context;

        private NullLoggerFactory loggingFactory;

        [SetUp]
        public void Setup()
        {
            loggingFactory = new NullLoggerFactory();
            context = new ContextManager<TestController>();
            instance = CreateRequestValidationAttribute();
        }

        [Test]
        public void OnActionExecutingError()
        {
            context.ModelStateDictionary.AddModelError("Error", "Error");
            instance.OnActionExecuting(context.ActionExecutedContext);
            Assert.IsInstanceOf<InvalidViewStateResponse>(context.ActionExecutedContext.Result);
        }

        [Test]
        public void OnActionExecuting()
        {
            instance.OnActionExecuting(context.ActionExecutedContext);
            Assert.IsNull(context.ActionExecutedContext.Result);
        }

        private RequestValidationAttribute CreateRequestValidationAttribute()
        {
            return new RequestValidationAttribute(loggingFactory);
        }
    }
}
