using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/BackgroundGradientConfig")]
    public class BackgroundGradientConfig : ScriptableObject
    {
        [Range(0.1f, 1.5f)] public float BrightnessScale = 1;
        public float TransitionTime = 1.0f;
    }
}
