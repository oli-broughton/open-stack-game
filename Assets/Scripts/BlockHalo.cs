using System.Collections;
using System.Collections.Generic;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// interface for creating a playable effect
    /// </summary>
    public interface IPlayableEffect
    {
        void Play(float delay);
    }

    /// <summary>
    /// Gameobject representing a XY plane halo that wraps around a block.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class BlockHalo : MonoBehaviour
    {
        [SerializeField] HaloConfig _haloConfig;
        MeshFilter _meshFilter;
        IPlayableEffect[] _haloEffects;

        void Awake()
        {
            _meshFilter = this.GetComponentAssert<MeshFilter>();
            _haloEffects = GetComponents<IPlayableEffect>();
        }

        public void Play(float delay = 0)
        {
            for (int i = 0; i < _haloEffects.Length; ++i)
            {
                _haloEffects[i].Play(delay);
            }
        }
        
        public void SetSize(Bounds blockBounds)
        {
            _meshFilter.sharedMesh = MeshUtils.GenerateBlockHaloMesh(blockBounds.size, _haloConfig.BlockHaloSize);
        }
    }
}
