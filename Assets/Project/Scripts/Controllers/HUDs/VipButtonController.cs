using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUDs
{
    internal class VipButtonController : MonoBehaviour
    {
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _vipService = ServiceProvider.GetRequiredService<IVipService>();
        }
        #endregion
    }
}
