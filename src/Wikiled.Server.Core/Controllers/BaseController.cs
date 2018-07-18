using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Wikiled.Server.Core.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ILogger Logger { get; }

        protected BaseController(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            Logger = loggerFactory.CreateLogger(GetType());
        }

        [Route("version")]
        [HttpGet]
        public string ServerVersion()
        {
            var version = $"{GetType().Name} Version: [{Assembly.GetCallingAssembly().GetName().Version}]";
            Logger.LogInformation("Version request: {0}", version);
            return version;
        }
    }
}
