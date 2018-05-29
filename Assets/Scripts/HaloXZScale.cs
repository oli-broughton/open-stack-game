using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Scales the halo on a XZ plane over time.
    /// </summary>
    [RequireComponent(typeof(BlockHalo))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class HaloXZScale : MonoBehaviour, IPlayableEffect
    {

        [SerializeField] HaloConfig _haloConfig;

        public void Play(float delay)
        {
            transform.DOScale(new Vector3(_haloConfig.PulseChangeToScale, 1, _haloConfig.PulseChangeToScale),
                              _haloConfig.PulseScaleChangeSeconds).SetDelay(delay);
        }
        
    }
}
