using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace Wikiled.Server.Core.Testing.Server
{
    public class ContextManager<T> where T : class
    {
        public ContextManager()
        {
            ActionArguments = new Dictionary<string, object>();
            ActionDescriptor = new Mock<ActionDescriptor>();
            RouteData = new Mock<RouteData>();
            ModelStateDictionary = new ModelStateDictionary();
            HttpContext = new Mock<HttpContext>();
            Response = new Mock<HttpResponse>();
            HttpContext.Setup(item => item.Response).Returns(Response.Object);
            Controller = new Mock<T>();
            var actionContext = new ActionContext(
                HttpContext.Object,
                RouteData.Object,
                ActionDescriptor.Object,
                ModelStateDictionary);
            ActionExecutedContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                ActionArguments,
                Controller.Object);
            HttpRequest = new Mock<HttpRequest>();
            HttpContext.Setup(item => item.Request).Returns(HttpRequest.Object);
            RequestDictionary = new HeaderDictionary();
            HttpRequest.Setup(item => item.Headers).Returns(RequestDictionary);
            ConnectionInfo = new DefaultConnectionInfo(new FeatureCollection());
            HttpContext.Setup(item => item.Connection).Returns(ConnectionInfo);
            HttpContextAccessor = new Mock<IHttpContextAccessor>();
            HttpContextAccessor.Setup(item => item.HttpContext).Returns(HttpContext.Object);
        }

        public Mock<HttpResponse> Response { get; }

        public DefaultConnectionInfo ConnectionInfo { get; }

        public Dictionary<string, object> ActionArguments { get; }

        public Mock<ActionDescriptor> ActionDescriptor { get; }

        public Mock<RouteData> RouteData { get; }

        public Mock<HttpRequest> HttpRequest { get; }

        public HeaderDictionary RequestDictionary { get; }

        public Mock<HttpContext> HttpContext { get; }

        public Mock<IHttpContextAccessor> HttpContextAccessor { get; }

        public ModelStateDictionary ModelStateDictionary { get; }

        public ActionExecutingContext ActionExecutedContext { get; }

        public Mock<T> Controller { get; }

    }
}
