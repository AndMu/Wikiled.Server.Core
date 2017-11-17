using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Wikiled.Core.Standard.Api.Client;
using Wikiled.Server.Core.Testing.Authentication;

namespace Wikiled.Server.Core.Testing.Server
{
    public class ServerWrapper : IDisposable
    {
        public ServerWrapper(IWebHostBuilder builder)
        {
            Server = new TestServer(builder);
            Client = Server.CreateClient();
            Client.Timeout = TimeSpan.MaxValue;
            ApiClient = new ApiClientFactory(Client, Client.BaseAddress).GetClient();
            StreamingClient = new RawApiClientFactory(Client, Client.BaseAddress).GetClient();
        }

        public TestServer Server { get; }

        public IApiClient ApiClient { get; }

        public HttpClient Client { get; }

        public IApiClient StreamingClient { get; }

        public static ServerWrapper Create<TStartup>(string root, Action<IServiceCollection> configureServices)
            where TStartup : class 
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(configureServices)
                .UseWebRoot(root)
                .UseContentRoot(root)
                .UseStartup<TStartup>();
            return new ServerWrapper(builder);
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
