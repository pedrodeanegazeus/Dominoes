using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Lobby
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField] private Button _moreGamesButton;

        private const string _moreGamesUrl = "https://play.google.com/store/apps/dev?id=6748044026530676626";

        private IGzLogger<HeaderView> _logger;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<HeaderView>>();
        }

        private void OnDisable()
        {
            _moreGamesButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _moreGamesButton.onClick.AddListener(OnMoreGamesButtonClick);
        }
        #endregion

        #region Events
        private void OnMoreGamesButtonClick()
        {
            _logger.LogMethodCall(nameof(OnMoreGamesButtonClick));

            Application.OpenURL(_moreGamesUrl);
        }
        #endregion
    }
}
