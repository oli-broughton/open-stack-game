using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/BlockConfig")]
    public class BlockConfig : ScriptableObject
    {
        public Vector3 BlockStartingSize = Vector3.one;
        public float BlockPingPongLoopTime = 1.0f;
        public float DestroyBlocksBelowHeight = -100.0f;
        public float BlockGrowToScale = 2.5f;
        public float BlockGrowTimeSeconds = 0.25f;
        public float BlockGrowDelaySeconds = 0.25f;
    }
}
