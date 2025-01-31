using Gazeus.CoreMobile.SDK.Core.Extensions;
using Gazeus.CoreMobile.SDK.Core.Interfaces;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Prefabs
{
    public class AvatarView : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Image _vipBorderImage;

        private IGzLogger<AvatarView> _logger;

        #region Unity
        private void Awake()
        {
            _logger = GameManager.ServiceProviderManager.GetService<IGzLogger<AvatarView>>();
        }
        #endregion

        public void SetAvatarSprite(Sprite sprite)
        {
            _logger.LogMethodCall(nameof(SetAvatarSprite));

            _avatarImage.sprite = sprite;
        }

        public void SetAvatarVip(bool isVip)
        {
            _logger.LogMethodCall(nameof(SetAvatarVip),
                                  isVip);

            _vipBorderImage.gameObject.SetActive(isVip);
        }
    }
}
