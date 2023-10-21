using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUD
{
    internal class ProfileController : MonoBehaviour
    {
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;

        private readonly IVipService _vipService = ServiceProvider.GetRequiredService<IVipService>();

        #region Unity
        private void Start()
        {
            SetVip();
        }
        #endregion

        private void SetVip()
        {
            _noVipBorder.SetActive(!_vipService.IsVip);
            _vipBorder.SetActive(_vipService.IsVip);
        }
    }
}
