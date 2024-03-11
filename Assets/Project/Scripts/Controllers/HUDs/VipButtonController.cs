using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using UnityEngine;

namespace Dominoes.Controllers.HUDs
{
    internal class VipButtonController : MonoBehaviour
    {
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
        }
        #endregion
    }
}
