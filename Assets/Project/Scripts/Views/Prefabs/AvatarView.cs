using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Prefabs
{
    public class AvatarView : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;
        [SerializeField] private Image _vipBorderImage;

        public void SetAvatarSprite(Sprite sprite)
        {
            _avatarImage.sprite = sprite;
        }

        public void SetAvatarVip(bool isVip)
        {
            _vipBorderImage.gameObject.SetActive(isVip);
        }
    }
}
