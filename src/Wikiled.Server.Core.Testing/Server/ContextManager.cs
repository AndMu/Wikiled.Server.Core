using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Wikiled.Server.Core.Testing.Server
{
    public class ContextManager<T> where T : ControllerBase
    {
        public ContextManager(T controller = null)
        {
            ActionArguments = new Dictionary<string, object>();
            ActionDescriptor = new Mock<ControllerActionDescriptor>();
            RouteData = new Mock<RouteData>();
            ModelStateDictionary = new ModelStateDictionary();
            HttpContext = new Mock<HttpContext>();
            Response = new Mock<HttpResponse>();
            HttpContext.Setup(item => item.Response).Returns(Response.Object);
            Controller = controller;
            if (controller == null)
            {
                ControllerMock = new Mock<T>();
                Controller = ControllerMock.Object;
            }

            var actionContext = new ActionContext(
                HttpContext.Object,
                RouteData.Object,
                ActionDescriptor.Object,
                ModelStateDictionary);
            ActionExecutedContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                ActionArguments,
                Controller);
            HttpRequest = new Mock<HttpRequest>();
            HttpContext.Setup(item => item.Request).Returns(HttpRequest.Object);
            RequestDictionary = new HeaderDictionary();
            HttpRequest.Setup(item => item.Headers).Returns(RequestDictionary);

            ConnectionInfo = new Mock<ConnectionInfo>();
            HttpContext.Setup(item => item.Connection).Returns(ConnectionInfo.Object);
            HttpContextAccessor = new Mock<IHttpContextAccessor>();
            HttpContextAccessor.Setup(item => item.HttpContext).Returns(HttpContext.Object);
            ControllerContext = new ControllerContext(actionContext);
            if (controller != null)
            {
                controller.ControllerContext = ControllerContext;
            }
        }

        public T Controller { get; }

        public ControllerContext ControllerContext { get; }

        public Mock<HttpResponse> Response { get; }

        public Mock<ConnectionInfo> ConnectionInfo { get; }

        public Dictionary<string, object> ActionArguments { get; }

        public Mock<ControllerActionDescriptor> ActionDescriptor { get; }

        public Mock<RouteData> RouteData { get; }

        public Mock<HttpRequest> HttpRequest { get; }

        public HeaderDictionary RequestDictionary { get; }

        public Mock<HttpContext> HttpContext { get; }

        public Mock<IHttpContextAccessor> HttpContextAccessor { get; }

        public ModelStateDictionary ModelStateDictionary { get; }

        public ActionExecutingContext ActionExecutedContext { get; }

        public Mock<T> ControllerMock { get; }

    }
}
