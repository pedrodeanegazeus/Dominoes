using Dominoes.Components;
using Dominoes.Core.Interfaces.Services;
using UnityEngine;

namespace Dominoes.Controllers
{
    internal class LobbyController : MonoBehaviour
    {
        [SerializeField] private GameObject _defaultLogoPrefab;
        [SerializeField] private GameObject _vipButton;
        [SerializeField] private ServiceProvider _serviceProvider;

        private IVipService VipService => _serviceProvider.GetRequiredService<IVipService>();

        public void OnUpdateAsset()
        {
            Destroy(_defaultLogoPrefab);
        }

        #region Unity
        private void Start()
        {
            _vipButton.SetActive(!VipService.IsVip);
        }
        #endregion
    }
}
