using UnityEngine;
using UnityEngine.EventSystems;

namespace Dominoes.Components
{
    [ExecuteAlways]
    /// <summary>
    /// Safe area implementation for notched mobile devices. Usage:
    ///  (1) Add this component to the top level of any GUI panel. 
    ///  (2) If the panel uses a full screen background image, then create an immediate child and put the component on that instead, with all other elements childed below it.
    ///      This will allow the background image to stretch to the full extents of the screen behind the notch, which looks nicer.
    ///  (3) For other cases that use a mixture of full horizontal and vertical background stripes, use the Conform X & Y controls on separate elements as needed.
    /// </summary>
    public class SafeArea : UIBehaviour
    {
        #region Simulations
        /// <summary>
        /// Simulation device that uses safe area due to a physical notch or software home bar. For use in Editor only.
        /// </summary>
        public enum SimDevice
        {
            /// <summary>
            /// Don't use a simulated safe area - GUI will be full screen as normal.
            /// </summary>
            None,
            /// <summary>
            /// Simulate the iPhone X and Xs (identical safe areas).
            /// </summary>
            iPhoneX,
            /// <summary>
            /// Simulate the iPhone Xs Max and XR (identical safe areas).
            /// </summary>
            iPhoneXsMax,
            /// <summary>
            /// Simulate the Google Pixel 3 XL using landscape left.
            /// </summary>
            Pixel3XL_LSL,
            /// <summary>
            /// Simulate the Google Pixel 3 XL using landscape right.
            /// </summary>
            Pixel3XL_LSR
        }

        /// <summary>
        /// Simulation mode for use in editor only. This can be edited at runtime to toggle between different safe areas.
        /// </summary>
        public static SimDevice Sim = SimDevice.None;

        /// <summary>
        /// Normalised safe areas for iPhone X with Home indicator (ratios are identical to iPhone Xs). Absolute values:
        ///  PortraitU x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436;
        ///  PortraitD x=0, y=102, w=1125, h=2202 on full extents w=1125, h=2436 (not supported, remains in Portrait Up);
        ///  LandscapeL x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125;
        ///  LandscapeR x=132, y=63, w=2172, h=1062 on full extents w=2436, h=1125.
        ///  Aspect Ratio: ~19.5:9.
        /// </summary>
        private readonly Rect[] _nsa_iPhoneX = new Rect[]
        {
            new Rect (0f, 102f / 2436f, 1f, 2202f / 2436f),  // Portrait
            new Rect (132f / 2436f, 63f / 1125f, 2172f / 2436f, 1062f / 1125f)  // Landscape
        };

        /// <summary>
        /// Normalised safe areas for iPhone Xs Max with Home indicator (ratios are identical to iPhone XR). Absolute values:
        ///  PortraitU x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688;
        ///  PortraitD x=0, y=102, w=1242, h=2454 on full extents w=1242, h=2688 (not supported, remains in Portrait Up);
        ///  LandscapeL x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242;
        ///  LandscapeR x=132, y=63, w=2424, h=1179 on full extents w=2688, h=1242.
        ///  Aspect Ratio: ~19.5:9.
        /// </summary>
        private readonly Rect[] _nsa_iPhoneXsMax = new Rect[]
        {
            new Rect (0f, 102f / 2688f, 1f, 2454f / 2688f),  // Portrait
            new Rect (132f / 2688f, 63f / 1242f, 2424f / 2688f, 1179f / 1242f)  // Landscape
        };

        /// <summary>
        /// Normalised safe areas for Pixel 3 XL using landscape left. Absolute values:
        ///  PortraitU x=0, y=0, w=1440, h=2789 on full extents w=1440, h=2960;
        ///  PortraitD x=0, y=0, w=1440, h=2789 on full extents w=1440, h=2960;
        ///  LandscapeL x=171, y=0, w=2789, h=1440 on full extents w=2960, h=1440;
        ///  LandscapeR x=0, y=0, w=2789, h=1440 on full extents w=2960, h=1440.
        ///  Aspect Ratio: 18.5:9.
        /// </summary>
        private readonly Rect[] _nsa_Pixel3XL_LSL = new Rect[]
        {
            new Rect (0f, 0f, 1f, 2789f / 2960f),  // Portrait
            new Rect (0f, 0f, 2789f / 2960f, 1f)  // Landscape
        };

        /// <summary>
        /// Normalised safe areas for Pixel 3 XL using landscape right. Absolute values and aspect ratio same as above.
        /// </summary>
        private readonly Rect[] _nsa_Pixel3XL_LSR = new Rect[]
        {
            new Rect (0f, 0f, 1f, 2789f / 2960f),  // Portrait
            new Rect (171f / 2960f, 0f, 2789f / 2960f, 1f)  // Landscape
        };
        #endregion

        [SerializeField] private bool _conformX;  // Conform to screen safe area on X-axis (default true, disable to ignore)
        [SerializeField] private bool _conformY;  // Conform to screen safe area on Y-axis (default true, disable to ignore)

        private RectTransform _panel;
        private Rect _lastSafeArea;

        #region Unity
        protected override void Awake()
        {
            _conformX = true;
            _conformY = true;
            _lastSafeArea = new Rect(0, 0, 0, 0);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!TryGetComponent(out _panel))
            {
                Debug.LogError($"Cannot apply safe area - no RectTransform found on {name}");
                Destroy(gameObject);
            }
            Handle();
        }

        protected virtual void Update()
        {
            Handle();
        }
        #endregion

        private void ApplySafeArea(Rect rect)
        {
            _lastSafeArea = rect;

            // Ignore x-axis?
            if (!_conformX)
            {
                rect.x = 0;
                rect.width = Screen.width;
            }

            // Ignore y-axis?
            if (!_conformY)
            {
                rect.y = 0;
                rect.height = Screen.height;
            }

            // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
            Vector2 anchorMin = rect.position;
            Vector2 anchorMax = rect.position + rect.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }

        private Rect GetSafeArea()
        {
            Rect safeArea = Screen.safeArea;
            if (Application.isEditor && Sim != SimDevice.None)
            {
                Rect nsa = new(0, 0, Screen.width, Screen.height);
                switch (Sim)
                {
                    case SimDevice.iPhoneX:
                        nsa = Screen.height > Screen.width
                            ? _nsa_iPhoneX[0]  // Portrait
                            : _nsa_iPhoneX[1]; // Landscape
                        break;
                    case SimDevice.iPhoneXsMax:
                        nsa = Screen.height > Screen.width
                            ? _nsa_iPhoneXsMax[0]  // Portrait
                            : _nsa_iPhoneXsMax[1]; // Landscape
                        break;
                    case SimDevice.Pixel3XL_LSL:
                        nsa = Screen.height > Screen.width
                            ? _nsa_Pixel3XL_LSL[0]  // Portrait
                            : _nsa_Pixel3XL_LSL[1]; // Landscape
                        break;
                    case SimDevice.Pixel3XL_LSR:
                        nsa = Screen.height > Screen.width
                            ? _nsa_Pixel3XL_LSR[0]  // Portrait
                            : _nsa_Pixel3XL_LSR[1]; // Landscape
                        break;
                    default:
                        break;
                }
                safeArea = new Rect(Screen.width * nsa.x, Screen.height * nsa.y, Screen.width * nsa.width, Screen.height * nsa.height);
            }
            return safeArea;
        }

        private void Handle()
        {
            Rect safeArea = GetSafeArea();
            if ((Application.isEditor && !Application.isPlaying) || safeArea != _lastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }
    }
}
