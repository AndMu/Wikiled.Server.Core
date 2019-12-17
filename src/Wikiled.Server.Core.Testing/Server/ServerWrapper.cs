using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Wikiled.Common.Net.Client;
using Wikiled.Server.Core.Testing.Authentication;

namespace Wikiled.Server.Core.Testing.Server
{
    public sealed class ServerWrapper : IDisposable
    {
        public ServerWrapper(IWebHostBuilder builder, ILoggerFactory loggerFactory)
        {
            Server = new TestServer(builder);
            Client = Server.CreateClient();
            Client.Timeout = TimeSpan.FromMinutes(15);
            ApiClient = new ApiClientFactory(Client, Client.BaseAddress).GetClient();
            StreamingClient = new StreamApiClient(Client, Client.BaseAddress, loggerFactory);
            SocketClient = Server.CreateWebSocketClient();
        }

        public TestServer Server { get; }

        public WebSocketClient SocketClient { get; }

        public IApiClient ApiClient { get; }

        public HttpClient Client { get; }

        public IStreamApiClient StreamingClient { get; }

        public static ServerWrapper Create<TStartup>(string root, Action<IServiceCollection> configureServices = null, Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate = null)
            where TStartup : class
        {
            if (configureDelegate == null)
            {
                configureDelegate = (hostingContext, config) => { };
            }
            
            var builder = new WebHostBuilder()
                .ConfigureTestServices(configureServices)
                .UseWebRoot(root)
                .UseContentRoot(root)
                .ConfigureAppConfiguration(configureDelegate)
                .ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                .UseNLog()
                .UseStartup<TStartup>();
            return new ServerWrapper(builder, new LoggerFactory());
        }

        public void AddUser(string name)
        {
            TestAuthenticationOptions.SetActiveUser(name);
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
