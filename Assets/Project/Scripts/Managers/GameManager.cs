using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gazeus.Mobile.Domino.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private GameSceneManager _gameSceneManager;
        [SerializeField] private ServiceProviderManager _serviceProviderManager;

        private static bool s_isInitialized;

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

        /// <summary>
        /// Load bootstrap if now loaded
        /// </summary>
        public static void EditorGoToBootstrap()
        {
            if (!s_isInitialized &&
                Application.platform is RuntimePlatform.OSXEditor or RuntimePlatform.WindowsEditor)
            {
                SceneManager.LoadScene(0);
            }
        }

        public static void Initialize()
        {
            ServiceProviderManager.Initialize();
            AudioManager.Initialize();
            GameSceneManager.Initialize();

            s_isInitialized = true;
        }
    }
}
