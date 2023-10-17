using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUDs
{
    internal class VipButtonController : MonoBehaviour
    {
        [Header("Provider")]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _vipService = _serviceProvider.GetRequiredService<IVipService>();
        }
        #endregion
    }
}
