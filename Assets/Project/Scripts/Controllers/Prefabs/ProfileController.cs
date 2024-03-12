using System.Threading.Tasks;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using Dominoes.Managers;
using Gazeus.CoreMobile.Commons.Core.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Dominoes.Controllers.Prefabs
{
    internal class ProfileController : MonoBehaviour
    {
        [SerializeField] private GameObject _noVipBorder;
        [SerializeField] private GameObject _vipBorder;
        [SerializeField] private TextMeshProUGUI _profileName;

        private IProfileService _profileService;
        private IVipService _vipService;

        #region Unity
        private void Awake()
        {
            _profileService = GameManager.ServiceProvider.GetRequiredService<IProfileService>();
            _vipService = GameManager.ServiceProvider.GetRequiredService<IVipService>();

            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnDestroy()
        {
            LocalizationSettings.SelectedLocaleChanged -= LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            SetProfileName();
        }

        private void Start()
        {
            SetVip();
        }
        #endregion

        #region Events
        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            SetProfileName();
        }
        #endregion

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
