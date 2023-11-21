using System.Threading.Tasks;
using Dominoes.Animations;
using Dominoes.Controllers.HUDs;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.ScriptableObjects;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class SideMenuCanvasController : MonoBehaviour
    {
        [SerializeField] private GameState _gameState;

        [Space]
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

        private IGzLogger<SideMenuCanvasController> _logger;
        private IProfileService _profileService;
        private IVipService _vipService;

        public void Initialize()
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<SideMenuCanvasController>>();
            _profileService = ServiceProvider.GetRequiredService<IProfileService>();
            _vipService = ServiceProvider.GetRequiredService<IVipService>();
        }

        public void Open()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(Open));

            _botDifficulty.SetState(_gameState.BotDifficulty);
            _audioToggle.SetState(_gameState.Audio);
            _slideAnimation.SlideIn();
        }

        #region Unity
        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;

            _closeButton.onClick.AddListener(CloseSideMenu);
            _botDifficulty.Clicked += BotDifficulty_Clicked;
            _audioToggle.Clicked += AudioToggle_Clicked;
            _versionNumber.text = "6.0.0";
        }

        private void OnEnable()
        {
            Task<string> task = _profileService.GetProfileNameAsync();
            StartCoroutine(task.WaitForTaskCompleteRoutine(task => _profileName.text = task.Result));
        }

        private void Start()
        {
            SetPlatform();
            SetVip();
        }
        #endregion

        #region Events
        private void AudioToggle_Clicked(bool state)
        {
            _gameState.Audio = state;
        }

        private void BotDifficulty_Clicked(int state)
        {
            _gameState.BotDifficulty = state;
        }

        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            if (gameObject.activeSelf)
            {
                Task<string> task = _profileService.GetProfileNameAsync();
                StartCoroutine(task.WaitForTaskCompleteRoutine(task => _profileName.text = task.Result));
            }
        }
        #endregion

        private void CloseSideMenu()
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

        private void SetVip()
        {
            _noVipBorder.SetActive(!_vipService.IsVip);
            _vipBorder.SetActive(_vipService.IsVip);
        }
    }
}
