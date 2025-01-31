using System;
using Gazeus.Mobile.Domino.Core.Enum;
using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Prefabs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class HeaderView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _moreGamesButton;
        [SerializeField] private Button _profileButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _profileNameText;

        [Header("Views")]
        [SerializeField] private AvatarView _avatarView;

        public event Action SettingsButtonClicked;

        private const string _moreGamesUrl = "https://play.google.com/store/apps/dev?id=6748044026530676626";

        #region Unity
        private void OnDisable()
        {
            _moreGamesButton.onClick.RemoveAllListeners();
            _profileButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _moreGamesButton.onClick.AddListener(OnMoreGamesButtonClick);
            _profileButton.onClick.AddListener(OnProfileButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        }
        #endregion

        public void SetAvatarSprite(Sprite avatarSprite)
        {
            _avatarView.SetAvatarSprite(avatarSprite);
        }

        public void SetAvatarVip(bool isVip)
        {
            _avatarView.SetAvatarVip(isVip);
        }

        public void SetProfileName(string profileName)
        {
            _profileNameText.text = profileName;
        }

        #region Events
        private static void OnMoreGamesButtonClick()
        {
            Application.OpenURL(_moreGamesUrl);
        }

        private static void OnProfileButtonClick()
        {
            GameManager.GameSceneManager.LoadScene(GameScene.Profile);
        }

        private void OnSettingsButtonClick()
        {
            SettingsButtonClicked?.Invoke();
        }
        #endregion
    }
}
