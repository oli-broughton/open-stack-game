using System;
using OB.Extensions;
using UnityEngine;
using OB.Events;

namespace OB.Game
{
    /// <summary>
    /// Blends between provided gradients sequentially.  
    /// </summary>
    public class GradientPicker : MonoBehaviour
    {
        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventVerticalGradient StartingGradientSelected;
            public GameEventVerticalGradient GradientChanged;
        }

        [SerializeField] InvokeEvents _invokeEvents;
        [SerializeField] GradientPalette _palette;

        int _paletteIndex = 0;
        int _currentShade = 0;

        public Color CurrentBlendedColour { get; private set; }
        public VerticalGradient CurrentGradient { get; private set; }

		void Awake()
		{
            CurrentGradient = _palette.Gradients[_paletteIndex];
            CurrentBlendedColour = CurrentGradient.BottomColour;
		}

        void Start()
        {
             _invokeEvents.StartingGradientSelected.Invoke(CurrentGradient);
        }

		public Color NextBlendedColour()
        {
            ++_currentShade;
            if (_currentShade > _palette.NoShades)
            {
                _paletteIndex = NextPaletteIndex(_paletteIndex);
                CurrentGradient = _palette.Gradients[_paletteIndex];
                _invokeEvents.GradientChanged.Invoke(CurrentGradient);
                _currentShade = 0;
            }
            CurrentBlendedColour = _palette.Gradients[_paletteIndex].BlendedColour(ShadeToRatio(_currentShade));
            return CurrentBlendedColour;
        }

        float ShadeToRatio(int shade)
        {
            return (float)shade / _palette.NoShades;
        }

        int NextPaletteIndex(int currentPaletteIndex)
        {
            return (currentPaletteIndex + 1 >= _palette.Gradients.Length ? 0 : currentPaletteIndex + 1);
        }
    }
}
