using System.Threading.Tasks;

namespace Wikiled.Server.Core.Helpers
{
    public interface IAsyncInitialiser
    {
        Task InitializeAsync();
    }
}
