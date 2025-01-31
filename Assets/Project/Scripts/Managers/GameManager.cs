using Gazeus.CoreMobile.SDK.Core.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.Mobile.Domino.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameSceneManager _gameSceneManager;
        [SerializeField] private ServiceProviderManager _serviceProviderManager;

        private IGzLogger<GameManager> _logger;

        public static AudioManager AudioManager { get; private set; }
        public static GameSceneManager GameSceneManager { get; private set; }
        public static ServiceProviderManager ServiceProviderManager { get; private set; }

        #region Unity
        private void Awake()
        {
            AudioManager = _audioManager;
            GameSceneManager = _gameSceneManager;
            ServiceProviderManager = _serviceProviderManager;

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        public void Initialize()
        {
            ServiceProviderManager.Initialize();
            AudioManager.Initialize();
            GameSceneManager.Initialize();

            _logger = ServiceProviderManager.GetService<IGzLogger<GameManager>>();
            _logger.Info("Initialized");
        }
    }
}
