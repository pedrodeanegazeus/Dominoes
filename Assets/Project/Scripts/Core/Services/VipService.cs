using Dominoes.Core.Interfaces.Services;
using Gazeus.CoreMobile.Commons.Core.Interfaces;

namespace Dominoes.Core.Services
{
    internal class VipService : IVipService
    {
        private readonly IGzLogger<VipService> _logger;

        public bool IsVip { get; private set; }

        public VipService(IGzLogger<VipService> logger)
        {
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Initialize));

            IsVip = false;
        }
    }
}
