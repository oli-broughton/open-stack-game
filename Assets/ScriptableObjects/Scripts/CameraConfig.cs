using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        public int StartHeight;
        public int HeightIncrement = 1;
        public int HeightOffset;
        public float HeightIncreaseTime = 1;
        public float MinZoom = 1;
        public float MaxZoom = 10;
        public float ZoomIncreaseTime = 1;
    }
}
