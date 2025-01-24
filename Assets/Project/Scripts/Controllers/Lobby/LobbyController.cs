using System.Diagnostics.CodeAnalysis;
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

        #region Unity
        [SuppressMessage("Minor Code Smell", "S2325:Methods and properties that don't access instance data should be static", Justification = "Unity Awake method")]
        private void Awake()
        {
            GameManager.EditorGoToBootstrap();
        }

        private void OnDisable()
        {
            _headerView.SettingsButtonClicked -= HeaderView_SettingsButtonClicked;
            _settingsView.CloseButtonClicked -= SettingsView_CloseButtonClicked;
            _settingsView.SlideOutCompleted -= SettingsView_SlideOutCompleted;
        }

        private void OnEnable()
        {
            _headerView.SettingsButtonClicked += HeaderView_SettingsButtonClicked;
            _settingsView.CloseButtonClicked += SettingsView_CloseButtonClicked;
            _settingsView.SlideOutCompleted += SettingsView_SlideOutCompleted;
        }

        private void Start()
        {
            _headerView.gameObject.SetActive(true);
            _settingsView.gameObject.SetActive(false);
        }
        #endregion

        #region Events
        private void HeaderView_SettingsButtonClicked()
        {
            _settingsView.gameObject.SetActive(true);
            _settingsView.Show();
        }

        private void SettingsView_CloseButtonClicked()
        {
            _settingsView.Hide();
        }

        private void SettingsView_SlideOutCompleted()
        {
            _settingsView.gameObject.SetActive(false);
        }
        #endregion
    }
}
