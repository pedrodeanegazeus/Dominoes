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

        private readonly IProfileService _profileService = ServiceProvider.GetRequiredService<IProfileService>();
        private readonly IVipService _vipService = ServiceProvider.GetRequiredService<IVipService>();

        #region Unity
        private void Awake()
        {
            LocalizationSettings.SelectedLocaleChanged += LocalizationSettings_SelectedLocaleChanged;
        }

        private void OnEnable()
        {
            Task<string> task = _profileService.GetProfileNameAsync();
            StartCoroutine(task.WaitForTaskCompleteRoutine(task => _profileName.text = task.Result));
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
                StartCoroutine(task.WaitForTaskCompleteRoutine(task => _profileName.text = task.Result));
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
