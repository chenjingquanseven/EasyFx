using System;
using EasyFx.Core.DependencyInjection;

namespace EasyFx.Core.Domain
{
    public interface IDistributionLock : ITransient
    {
        void Release(string resource);
        LockResult Lock(string resource, bool isWait = false, int timeout = 5000);
    }

    public class LockResult : IDisposable
    {
        public string Resource { get; set; }
        public IDistributionLock DistributionLock { get; set; }
        public bool IsAcquired { get; set; }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (IsAcquired)
            {
                DistributionLock?.Release(Resource);
            }
        }
    }
}