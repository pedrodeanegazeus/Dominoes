using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUD
{
    internal class ProfileController : MonoBehaviour
    {
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;
        [SerializeField] private ServiceProvider _serviceProvider;

        private IVipService VipService => _serviceProvider.GetRequiredService<IVipService>();

        #region Unity
        private void Start()
        {
            _noVipBorder.SetActive(!VipService.IsVip);
            _vipBorder.SetActive(VipService.IsVip);
        }
        #endregion
    }
}
