using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class HeaderView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _moreGamesButton;
        [SerializeField] private Button _settingsButton;

        public event Action SettingsButtonClicked;

        private const string _moreGamesUrl = "https://play.google.com/store/apps/dev?id=6748044026530676626";

        #region Unity
        private void OnDisable()
        {
            _moreGamesButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _moreGamesButton.onClick.AddListener(OnMoreGamesButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }
        #endregion

        #region Events
        private static void OnMoreGamesButtonClick()
        {
            Application.OpenURL(_moreGamesUrl);
        }

        private void OnSettingsButtonClick()
        {
            SettingsButtonClicked?.Invoke();
        }
        #endregion
    }
}
