using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Lobby;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Controllers.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private HeaderView _headerView;
        [SerializeField] private SettingsView _settingsView;

        private SettingsController _settingsController;

        #region Unity
        private void Awake()
        {
            GameManager.EditorGoToBootstrap();

            _settingsController = GameManager.ServiceProviderManager.GetService<SettingsController>();
        }

        private void OnDisable()
        {
            _headerView.SettingsButtonClicked -= HeaderView_SettingsButtonClicked;
        }

        private void OnEnable()
        {
            _headerView.SettingsButtonClicked += HeaderView_SettingsButtonClicked;
        }

        private void Start()
        {
            _headerView.gameObject.SetActive(true);

            _settingsController.Initialize(_settingsView);
        }
        #endregion

        #region Events
        private async void HeaderView_SettingsButtonClicked()
        {
            await _settingsController.ShowAsync();
        }
        #endregion
    }
}
