using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers.HUD
{
    internal class ProfileController : MonoBehaviour
    {
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;

        [Space]
        [SerializeField] private DominoesServiceProvider _serviceProvider;

        private IVipService VipService { get; set; }

        public void UpdateView()
        {
            _noVipBorder.SetActive(!VipService.IsVip);
            _vipBorder.SetActive(VipService.IsVip);
        }

        #region Unity
        private void Awake()
        {
            VipService = _serviceProvider.GetRequiredService<IVipService>();
        }

        private void Start()
        {
            UpdateView();
        }
        #endregion
    }
}
