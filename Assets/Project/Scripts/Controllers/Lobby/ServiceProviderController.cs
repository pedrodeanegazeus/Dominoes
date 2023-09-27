using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Services;
using UnityEngine;

namespace Dominoes.Controllers.Lobby
{
    internal class ServiceProviderController : MonoBehaviour
    {
        private ServiceProvider _serviceProvider;

        #region Unity
        private void Awake()
        {
            _serviceProvider = GetComponent<ServiceProvider>();
            _serviceProvider.Initialize();
            _serviceProvider.AddTransient<IVipService, VipService>();
            _serviceProvider.Build();
        }
        #endregion
    }
}
