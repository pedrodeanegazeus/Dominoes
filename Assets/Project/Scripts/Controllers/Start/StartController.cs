using Dominoes.Core.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dominoes.Controllers
{
    public class StartController : MonoBehaviour
    {
        [SerializeField] private AnimationController _animationController;

        #region Unity
        private void Awake()
        {
            _animationController.EventFired += AnimationController_EventFired;
        }
        #endregion

        private void AnimationController_EventFired(string @event)
        {
            switch (@event)
            {
                case "Jogatina_End":
                    _ = SceneManager.LoadSceneAsync(nameof(DominoesScene.Lobby));
                    break;
            }
        }
    }
}
