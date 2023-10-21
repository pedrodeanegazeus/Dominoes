using System.Threading.Tasks;
using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Services
{
    internal class VipService : IVipService
    {
        private readonly IGzLogger<VipService> _logger;

        public bool IsVip { get; private set; }

        public VipService(IGzLogger<VipService> logger)
        {
            _logger = logger;
        }

        public Task InitializeAsync()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(InitializeAsync));

            IsVip = false;
            return Task.CompletedTask;
        }
    }
}
