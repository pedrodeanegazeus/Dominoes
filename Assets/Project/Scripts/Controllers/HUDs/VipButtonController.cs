using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using UnityEngine;

namespace Dominoes.Controllers.HUDs
{
    internal class VipButtonController : MonoBehaviour
    {
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _vipService = ServiceProviderManager.Instance.GetRequiredService<IVipService>();
        }
        #endregion
    }
}
