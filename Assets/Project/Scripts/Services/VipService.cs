using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;

namespace Dominoes.Services
{
    internal class VipService : IVipService
    {
        public bool IsVip { get; private set; }

        public Task InitializeAsync()
        {
            IsVip = false;
            return Task.CompletedTask;
        }
    }
}
