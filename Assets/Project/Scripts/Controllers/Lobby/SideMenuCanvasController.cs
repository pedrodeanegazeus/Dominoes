using Dominoes.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class SideMenuCanvasController : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private SlideInAnimation _slideInAnimation;
        [SerializeField] private SlideOutAnimation _slideOutAnimation;

        [Header("Header")]
        [SerializeField] private Button _closeButton;

        public void OpenSideMenu()
        {
            gameObject.SetActive(true);
            _slideInAnimation.SlideIn();
        }

        #region Unity
        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseSideMenu);
        }
        #endregion

        private void CloseSideMenu()
        {
            _slideOutAnimation.SlideOut(() => gameObject.SetActive(false));
        }
    }
}
