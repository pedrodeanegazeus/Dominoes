using System.Threading.Tasks;
using Dominoes.Animations;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
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
        [SerializeField] private SlideAnimation _slideAnimation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;
        [SerializeField] private TextMeshProUGUI _profileName;
        [SerializeField] private GameObject _androidItems;
        [SerializeField] private GameObject _iosItems;
        [SerializeField] private GameObject _logoutItem;

        private readonly IGzLogger<SideMenuCanvasController> _logger = ServiceProvider.GetRequiredService<IGzLogger<SideMenuCanvasController>>();
        private readonly IProfileService _profileService = ServiceProvider.GetRequiredService<IProfileService>();
        private readonly IVipService _vipService = ServiceProvider.GetRequiredService<IVipService>();

        public void OpenSideMenu()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(OpenSideMenu));

            _slideAnimation.SlideIn();
        }

        public void Test()
        {
            Debug.Log("Clicked");
        }

        #region Unity
        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;

            _closeButton.onClick.AddListener(CloseSideMenu);
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
