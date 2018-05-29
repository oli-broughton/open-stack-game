using DG.Tweening;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Fades the halo over time,.
    /// </summary>
    [RequireComponent(typeof(BlockHalo))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class HaloFade : MonoBehaviour, IPlayableEffect
    {
        [SerializeField] HaloConfig _haloConfig;

        Material _material;

        void Awake()
        {
            _material = this.GetComponentAssert<MeshRenderer>().material;
        }

        public void Play(float delay)
        {
            _material.DOFade(0, _haloConfig.FadeTimeSeconds).SetDelay(delay);
        }
    }
}
