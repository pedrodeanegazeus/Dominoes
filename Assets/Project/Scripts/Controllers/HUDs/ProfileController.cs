using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUD
{
    internal class ProfileController : MonoBehaviour
    {
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;

        [Header("Provider")]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _vipService = _serviceProvider.GetRequiredService<IVipService>();
        }

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
