using UnityEngine;

namespace OB
{
    [System.Serializable]
    public class VerticalGradient
    {
        public Color TopColour = Color.white;
        public Color BottomColour = Color.black;

        public VerticalGradient()
        {
            
        }

        public VerticalGradient(Color topColour, Color bottomColour)
        {
            this.TopColour = topColour;
            this.BottomColour = bottomColour;
        }

        public Color BlendedColour(float time)
        {
            return Color.Lerp(BottomColour, TopColour, time);
        }
    }

}