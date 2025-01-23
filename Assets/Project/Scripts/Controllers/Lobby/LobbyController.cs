using Gazeus.Mobile.Domino.Managers;
using Gazeus.Mobile.Domino.Views.Lobby;
using UnityEngine;

namespace Gazeus.Mobile.Domino.Controllers.Lobby
{
    public class LobbyController : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private HeaderView _headerView;

        #region Unity
        private void Awake()
        {
            GameManager.EditorGoToBootstrap();
        }
        #endregion
    }
}
