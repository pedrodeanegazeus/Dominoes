using System.Threading.Tasks;
using Dominoes.Core.Extensions;
using Dominoes.Core.Interfaces.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Dominoes.Controllers.HUD
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
            _profileService = ServiceProvider.GetRequiredService<IProfileService>();
            _vipService = ServiceProvider.GetRequiredService<IVipService>();

            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            Task<string> task = _profileService.GetProfileNameAsync();
            _ = StartCoroutine(task.WaitForTaskCompleteRoutine(result => _profileName.text = result));
        }

        private void Start()
        {
            SetVip();
        }
        #endregion

        #region Events
        private void LocalizationSettings_SelectedLocaleChanged(Locale locale)
        {
            if (gameObject.activeSelf)
            {
                Task<string> task = _profileService.GetProfileNameAsync();
                _ = StartCoroutine(task.WaitForTaskCompleteRoutine(result => _profileName.text = result));
            }
        }
        #endregion

        private void SetVip()
        {
            _noVipBorder.SetActive(!_vipService.IsVip);
            _vipBorder.SetActive(_vipService.IsVip);
        }
    }
}
