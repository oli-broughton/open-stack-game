using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/HaloConfig")]
    public class HaloConfig : ScriptableObject
    {
        public float BlockHaloSize = 0.25f;
        public float FadeTimeSeconds = 1.0f;
        public float BlockHaloPulseDelaySeconds = 0.1f;
        public float PulseChangeToScale = 2.2f;
        public float PulseScaleChangeSeconds = 1.0f;
    }
}
