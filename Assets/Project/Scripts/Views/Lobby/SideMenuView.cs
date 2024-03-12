using System.Threading.Tasks;
using Dominoes.Animations;
using Dominoes.Controllers.Prefabs;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Repositories;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Core.Models;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Dominoes.Views.Lobby
{
    internal class SideMenuView : MonoBehaviour
    {
        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _versionNumber;

        [Header("Profile")]
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;
        [SerializeField] private TextMeshProUGUI _profileName;

        [Header("Menu")]
        [SerializeField] private TripleButtonController _botDifficulty;
        [SerializeField] private ToggleButtonController _audioToggle;
        [SerializeField] private GameObject _androidItems;
        [SerializeField] private GameObject _iosItems;
        [SerializeField] private GameObject _logoutItem;

        private IGameConfigRepository _gameConfigRepository;
        private IGzLogger<SideMenuView> _logger;
        private IProfileService _profileService;
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;

            _closeButton.onClick.AddListener(Close);
            _botDifficulty.Clicked += BotDifficulty_Clicked;
            _audioToggle.Clicked += AudioToggle_Clicked;
            _versionNumber.text = "6.0.0";
        }

        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;

            _closeButton.onClick.RemoveAllListeners();
            _botDifficulty.Clicked -= BotDifficulty_Clicked;
            _audioToggle.Clicked -= AudioToggle_Clicked;
        }

        private void OnEnable()
        {
            SetProfileName();
        }

        private void Start()
        {
            SetPlatform();
            SetVip();
        }
        #endregion

        public void Initialize()
        {
            _gameConfigRepository = GameManager.ServiceProvider.GetRequiredService<IGameConfigRepository>();
            _logger = GameManager.ServiceProvider.GetRequiredService<IGzLogger<SideMenuView>>();
            _profileService = GameManager.ServiceProvider.GetRequiredService<IProfileService>();
            _vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();
        }

        public void Open()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Open));

            GameConfig gameConfig = _gameConfigRepository.GameConfig;
            _botDifficulty.SetState(gameConfig.BotDifficulty);
            _audioToggle.SetState(gameConfig.Audio);
            _slideAnimation.SlideIn();
        }

        #region Events
        private void AudioToggle_Clicked(bool state)
        {
            _gameConfigRepository.GameConfig.Audio = state;
            _gameConfigRepository.Sync();
        }

        private void BotDifficulty_Clicked(int state)
        {
            _gameConfigRepository.GameConfig.BotDifficulty = state;
            _gameConfigRepository.Sync();
        }

        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            SetProfileName();
        }
        #endregion

        private void Close()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }

        private void SetPlatform()
        {
#if UNITY_ANDROID
            _androidItems.SetActive(true);
            _iosItems.SetActive(false);
#elif UNITY_IOS
            _androidItems.SetActive(false);
            _iosItems.SetActive(true);
#endif
        }

        private void SetProfileName()
        {
            Task<string> task = _profileService.GetProfileNameAsync();
            _ = StartCoroutine(task.WaitTask(result => _profileName.text = result));
        }

        private void SetVip()
        {
            _noVipBorder.SetActive(!_vipService.IsVip);
            _vipBorder.SetActive(_vipService.IsVip);
        }
    }
}
