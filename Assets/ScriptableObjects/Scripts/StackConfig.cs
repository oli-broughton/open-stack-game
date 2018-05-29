using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    public enum XZAxis { X_AXIS, Z_AXIS }

    [CreateAssetMenu(menuName = "Data/StackConfig")]
    public class StackConfig : ScriptableObject
    {
        public int StartingStackHeight = 1;
        public XZAxis StartingSpawnAxis = XZAxis.X_AXIS;
        public float SpawnDistanceFromStack = 4;
    }
}