using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Prefabs
{
    public class AvatarView : MonoBehaviour
    {
        [SerializeField] private Image _vipBorderImage;

        private IGzLogger<AvatarView> _logger;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<AvatarView>>();
        }
        #endregion

        public void SetVIP(bool isVIP)
        {
            _logger.LogMethodCall(nameof(SetVIP),
                                  isVIP);

            _vipBorderImage.gameObject.SetActive(isVIP);
        }
    }
}
