using Gazeus.CoreMobile.Commons.Core.Interfaces;
using UnityEngine;

namespace Dominoes.Views.Gameplay
{
    internal class TileView : MonoBehaviour
    {
        [SerializeField] private bool _isVisible;
        [SerializeField] private int _topPeeps;
        [SerializeField] private int _bottomPeeps;

        [Space]
        [SerializeField] private GameObject _topPeep1;
        [SerializeField] private GameObject _topPeep2;
        [SerializeField] private GameObject _topPeep3;
        [SerializeField] private GameObject _topPeep4;
        [SerializeField] private GameObject _topPeep5;
        [SerializeField] private GameObject _topPeep6;
        [SerializeField] private GameObject _topPeep7;

        [Space]
        [SerializeField] private GameObject _divider;

        [Space]
        [SerializeField] private GameObject _bottomPeep1;
        [SerializeField] private GameObject _bottomPeep2;
        [SerializeField] private GameObject _bottomPeep3;
        [SerializeField] private GameObject _bottomPeep4;
        [SerializeField] private GameObject _bottomPeep5;
        [SerializeField] private GameObject _bottomPeep6;
        [SerializeField] private GameObject _bottomPeep7;

        private IGzLogger<TileView> _logger;

        public int TopPeeps => _topPeeps;
        public int BottomPeeps => _bottomPeeps;

        public void Initialize(int topPeeps, int bottomPeeps, bool isVisible)
        {
            _logger = ServiceProvider.GetRequiredService<IGzLogger<TileView>>();

            _topPeeps = topPeeps;
            _bottomPeeps = bottomPeeps;

            if (isVisible)
            {
                SetActive();
            }
        }

        public void ShowTile()
        {
            _logger.Debug("CALLED: {method}",
                          nameof(ShowTile));

            _isVisible = true;
            SetActive();
        }

        private void SetActive()
        {
            _divider.SetActive(true);

            switch (_topPeeps)
            {
                case 1:
                    _topPeep4.SetActive(true);
                    break;
                case 2:
                    _topPeep1.SetActive(true);
                    _topPeep7.SetActive(true);
                    break;
                case 3:
                    _topPeep1.SetActive(true);
                    _topPeep4.SetActive(true);
                    _topPeep7.SetActive(true);
                    break;
                case 4:
                    _topPeep1.SetActive(true);
                    _topPeep2.SetActive(true);
                    _topPeep6.SetActive(true);
                    _topPeep7.SetActive(true);
                    break;
                case 5:
                    _topPeep1.SetActive(true);
                    _topPeep2.SetActive(true);
                    _topPeep4.SetActive(true);
                    _topPeep6.SetActive(true);
                    _topPeep7.SetActive(true);
                    break;
                case 6:
                    _topPeep1.SetActive(true);
                    _topPeep2.SetActive(true);
                    _topPeep3.SetActive(true);
                    _topPeep5.SetActive(true);
                    _topPeep6.SetActive(true);
                    _topPeep7.SetActive(true);
                    break;
            }

            switch (_bottomPeeps)
            {
                case 1:
                    _bottomPeep4.SetActive(true);
                    break;
                case 2:
                    _bottomPeep1.SetActive(true);
                    _bottomPeep7.SetActive(true);
                    break;
                case 3:
                    _bottomPeep1.SetActive(true);
                    _bottomPeep4.SetActive(true);
                    _bottomPeep7.SetActive(true);
                    break;
                case 4:
                    _bottomPeep1.SetActive(true);
                    _bottomPeep2.SetActive(true);
                    _bottomPeep6.SetActive(true);
                    _bottomPeep7.SetActive(true);
                    break;
                case 5:
                    _bottomPeep1.SetActive(true);
                    _bottomPeep2.SetActive(true);
                    _bottomPeep4.SetActive(true);
                    _bottomPeep6.SetActive(true);
                    _bottomPeep7.SetActive(true);
                    break;
                case 6:
                    _bottomPeep1.SetActive(true);
                    _bottomPeep2.SetActive(true);
                    _bottomPeep3.SetActive(true);
                    _bottomPeep5.SetActive(true);
                    _bottomPeep6.SetActive(true);
                    _bottomPeep7.SetActive(true);
                    break;
            }
        }
    }
}
