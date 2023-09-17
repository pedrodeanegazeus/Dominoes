using UnityEngine;

namespace Dominoes.Controllers
{
    public class LobbyController : MonoBehaviour
    {
        [SerializeField] private GameObject _defaultLogoPrefab;

        public void OnUpdateAsset()
        {
            Destroy(_defaultLogoPrefab);
        }
    }
}
