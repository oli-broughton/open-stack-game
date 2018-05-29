using DG.Tweening;
using OB.Events;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Camera effect that renders a fullscreen vertical gradient.
    /// Gradients are generated using a top and bottom colour.
    /// </summary>
    public class VerticalGradientCameraEffect : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventVerticalGradient SetGradient;
            public GameEventVerticalGradient TransitionToGradient;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] BackgroundGradientConfig _backgroundGradientConfig;
        [SerializeField] Material _effectMaterial = null;

        readonly string TopColourProp = "_TopColour";
        readonly string BottomColourProp = "_BottomColour";
        
        void OnEnable()
        {
            _responseEvents.SetGradient.AddListener(SetGradient);
            _responseEvents.TransitionToGradient.AddListener(TransitionToGradient);
        }

        void OnDisable()
        {
            _responseEvents.SetGradient.RemoveListener(SetGradient);
            _responseEvents.TransitionToGradient.RemoveListener(TransitionToGradient);
        }

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _effectMaterial);
        }

        void SetGradient(VerticalGradient gradient)
        {
            _effectMaterial.SetColor(TopColourProp, ScaleBrightness(gradient.TopColour,_backgroundGradientConfig.BrightnessScale));
            _effectMaterial.SetColor(BottomColourProp, ScaleBrightness(gradient.BottomColour,_backgroundGradientConfig.BrightnessScale));
        }

        void TransitionToGradient(VerticalGradient gradient)
        {
            _effectMaterial.DOColor(ScaleBrightness(gradient.TopColour,_backgroundGradientConfig.BrightnessScale), TopColourProp,_backgroundGradientConfig.TransitionTime);
            _effectMaterial.DOColor(ScaleBrightness(gradient.BottomColour,_backgroundGradientConfig.BrightnessScale), BottomColourProp,_backgroundGradientConfig.TransitionTime);
        }

        Color ScaleBrightness(Color colour, float scale)
        {
            float h, s, v;
            Color.RGBToHSV(colour, out h, out s, out v);
            v *= 1.0f * scale;
            return Color.HSVToRGB(h, s, v);
        }
    }
}
