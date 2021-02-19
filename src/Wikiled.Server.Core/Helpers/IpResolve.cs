using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Wikiled.Server.Core.Helpers
{
    public class IpResolve : IIpResolve
    {
        private readonly HttpContext context;

        public IpResolve(IHttpContextAccessor contextAcccessor)
        {
            if (contextAcccessor == null)
            {
                throw new ArgumentNullException(nameof(contextAcccessor));
            }

            context = contextAcccessor.HttpContext;
        }

        public IpResolve(HttpContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetRequestIp(bool tryUseXForwardHeader = true)
        {
            string ip = null;
            if (tryUseXForwardHeader)
            {
                ip = GetHeaderValueAs<string>("X-Forwarded-For").SplitCsv().FirstOrDefault();
            }

            if (string.IsNullOrWhiteSpace(ip) && context?.Connection?.RemoteIpAddress != null)
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = GetHeaderValueAs<string>("REMOTE_ADDR");
            }

            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = "Failed to resolve IP";
            }

            return ip;
        }

        private T GetHeaderValueAs<T>(string headerName)
        {
            if (context != null &&
                context.Request.Headers.TryGetValue(headerName, out var values))
            {
                string rawValues = values.ToString();  
                if (!string.IsNullOrEmpty(rawValues))
                {
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
                }
            }

            return default;
        }
    }
}
