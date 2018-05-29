using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/DifficultyConfig")]
    public class DifficultyConfig : ScriptableObject
    {

        [Tooltip("Number of perfect blocks required to be placed consecutively before the player is rewarded with the block size increasing.")]
        public int NoConsecutivePerfectPlacements = 8;
        [Range(0.01f, 0.2f), Tooltip("Larger error margins will make it easier to place perfect blocks on to the stack.")]
        public float PerfectPlacementErrorMargin = 0.01f;
        public int ScoreIncrementAmount = 1;
    }
}
