using Dominoes.Animations;
using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Dominoes.Controllers.Lobby
{
    internal class SideMenuCanvasController : MonoBehaviour
    {
        [Header("Menu")]
        [SerializeField] private SlideAnimation _slideAnimation;

        [Header("Header")]
        [SerializeField] private Button _closeButton;

        private readonly IGzLogger<SideMenuCanvasController> _logger = ServiceProvider.GetRequiredService<IGzLogger<SideMenuCanvasController>>();

        public void OpenSideMenu()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(OpenSideMenu));

            gameObject.SetActive(true);
            _slideAnimation.SlideIn();
        }

        #region Unity
        private void Awake()
        {
            _closeButton.onClick.AddListener(CloseSideMenu);
        }
        #endregion

        private void CloseSideMenu()
        {
            _slideAnimation.SlideOut(() => gameObject.SetActive(false));
        }
    }
}
