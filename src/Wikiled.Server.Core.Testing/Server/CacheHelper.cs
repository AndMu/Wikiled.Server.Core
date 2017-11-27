using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Wikiled.Server.Core.Testing.Server
{
    public class CacheHelper
    {
        public CacheHelper()
        {
            MemoryCache = new Mock<IMemoryCache>();
            Entry = new Mock<ICacheEntry>();
            MemoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(Entry.Object);

        }

        public Mock<ICacheEntry> Entry { get; }

        public Mock<IMemoryCache> MemoryCache { get; }
    }
}
