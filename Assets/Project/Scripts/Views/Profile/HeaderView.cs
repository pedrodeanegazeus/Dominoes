using Gazeus.Mobile.Domino.Core.Enum;
using Gazeus.Mobile.Domino.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Gazeus.Mobile.Domino.Views.Profile
{
    public class HeaderView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button _backButton;

        #region Unity
        private void OnDisable()
        {
            _backButton.onClick.RemoveAllListeners();
        }

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnBackButtonClick);
        }
        #endregion

        #region Events
        private void OnBackButtonClick()
        {
            GameManager.GameSceneManager.LoadScene(GameScene.Lobby);
        }
        #endregion
    }
}
