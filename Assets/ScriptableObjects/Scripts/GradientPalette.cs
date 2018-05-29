using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB.Game
{
    [CreateAssetMenu(menuName = "Data/GradientPalette")]
    public class GradientPalette : ScriptableObject
    {
        public int NoShades = 10;
        public VerticalGradient[] Gradients = new VerticalGradient[1];
    }
}