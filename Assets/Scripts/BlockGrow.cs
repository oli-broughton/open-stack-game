using UnityEngine;
using DG.Tweening;
using System;
using OB.Extensions;
using OB.Events;
using UnityEngine.Events;

namespace OB.Game
{
    /// <summary>
    /// Increase the size of a block over time.
    /// </summary>
    [RequireComponent(typeof(Block))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(BoxCollider))]
    public class BlockGrow : MonoBehaviour
    {
        [SerializeField] GameEvent _onStart;
        [SerializeField] BlockConfig _blockConfig;

        BoxCollider _boxCollider;
        MeshFilter _meshFilter;
        float _growScale = 0;

        void Awake()
        {
            _boxCollider = this.GetComponentAssert<BoxCollider>();
            _meshFilter = this.GetComponentAssert<MeshFilter>();
        }

        public void Grow(Vector3 maxSize, Action onGrowComplete = null)
        {
            Vector3 maxExtents = maxSize * 0.5f;
            Bounds blockBounds = _boxCollider.bounds;
            Vector3 min = blockBounds.min, max = blockBounds.max;
            DOTween.To(() => _growScale, x => _growScale = x, 1.0f, _blockConfig.BlockGrowTimeSeconds).SetEase(Ease.OutBack).SetDelay(_blockConfig.BlockGrowDelaySeconds)
                   .OnStart(_onStart.Invoke)
                   .OnUpdate(() =>
            {

                blockBounds.SetMinMax(new Vector3(Mathf.Max(min.x - _blockConfig.BlockGrowToScale * _growScale, -maxExtents.x), min.y, Mathf.Max(min.z - _blockConfig.BlockGrowToScale * _growScale, -maxExtents.z)),
                                      new Vector3(Mathf.Min(max.x + _blockConfig.BlockGrowToScale * _growScale * _growScale, maxExtents.x), max.y, Mathf.Min(max.z + _blockConfig.BlockGrowToScale * _growScale, maxExtents.z)));

                MeshUtils.UpdateBlockMeshSize(_meshFilter.mesh, blockBounds.size);
                transform.position = new Vector3(blockBounds.center.x, transform.position.y, blockBounds.center.z);
                _boxCollider.size = _meshFilter.mesh.bounds.size;
            }).OnComplete(() =>
            {
                if (onGrowComplete != null)
                    onGrowComplete();
            });
        }
    }
}
