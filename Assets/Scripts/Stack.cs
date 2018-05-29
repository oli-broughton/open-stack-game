using System;
using OB.Events;
using UnityEngine;
using DG.Tweening;

namespace OB.Game
{
    /// <summary>
    /// Main game logic for the stack.
    /// </summary>
    [RequireComponent(typeof(BlockFactory))]
    public class Stack : MonoBehaviour
    {

        [System.Serializable]
        public class ResponseEvents
        {
            public GameEvent SpawnNextBlock;
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEvent BlockPlaced;
            public GameEventInt PerfectBlockPlaced;
            public GameEvent BlockCut;
            public GameEvent BlockMissed;
        }

        [SerializeField] BlockConfig _blockConfig;
        [SerializeField] DifficultyConfig _diffConfig;
        [SerializeField] StackConfig _stackConfig;

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;

        BlockFactory _blockFactory;
        int _perfectPlacementCount;
        int _perfectPlacementsLevel1;
        int _perfectPlacementsLevel2;
        XZAxis _movementAxis;
        Block _topBlock;
        Block _movingBlock;

        void Start()
        {
            // split perfect placements into different levels
            _perfectPlacementsLevel1 = _diffConfig.NoConsecutivePerfectPlacements / 2;
            _perfectPlacementsLevel2 = _diffConfig.NoConsecutivePerfectPlacements - _perfectPlacementsLevel1;
            GenerateInitialStack();
        }

        void Awake()
        {
            _blockFactory = GetComponent<BlockFactory>();
        }

        void OnEnable()
        {
            _responseEvents.SpawnNextBlock.AddListener(NextBlock);
        }

        void OnDisable()
        {
            _responseEvents.SpawnNextBlock.RemoveListener(NextBlock);
        }

        public bool IsOverlap()
        {
            if (_movingBlock == null)
                return false;
            Bounds movingBlockBounds = _movingBlock.Bounds;
            Bounds topBlockBounds = _topBlock.Bounds;
            return BlockUtils.Overlap(ref movingBlockBounds, ref topBlockBounds);
        }

        public bool IsPerfectOverlap()
        {
            if (_movingBlock == null)
                return false;
            Bounds movingBlockBounds = _movingBlock.Bounds;
            Bounds topBlockBounds = _topBlock.Bounds;
            return BlockUtils.PerfectOverlap(ref movingBlockBounds, ref topBlockBounds, _diffConfig.PerfectPlacementErrorMargin);
        }

        public void PlaceBlock()
        {
            Bounds movingBlockBounds = _movingBlock.Bounds;
            Bounds topBlockBounds = _topBlock.Bounds;

            _movingBlock.DOKill();
            Destroy(_movingBlock.gameObject);

            // Check to see if the block overlaps with the top of the stack
            if (BlockUtils.Overlap(ref movingBlockBounds, ref topBlockBounds))
            {
                // Check to see if the block was placed perfectly over the stack (within the allowed error margin).
                if (BlockUtils.PerfectOverlap(ref movingBlockBounds, ref topBlockBounds, _diffConfig.PerfectPlacementErrorMargin))
                {
                    _invokeEvents.PerfectBlockPlaced.Invoke(_perfectPlacementCount);
                    ++_perfectPlacementCount;

                    Bounds newTopBounds = BlockUtils.CalculateAboveBounds(ref topBlockBounds);

                    // New perfect placement levels are reached by consecutively placing perfect blocks.
                    // Once level 3 has been reached the player is rewarded by increasing the block size by a set amount. (if the block was previously chopped)
                    if (_perfectPlacementCount < _perfectPlacementsLevel1) // level 1
                    {
                        _topBlock = _blockFactory.GenerateStaticBlockFadeHalo(ref newTopBounds, transform);
                        NextBlock();
                    }
                    else if (_perfectPlacementCount < _perfectPlacementsLevel1 + _perfectPlacementsLevel2) // level 2
                    {
                        _topBlock = _blockFactory.GenerateStaticBlockPulseHalo(ref newTopBounds, _perfectPlacementCount - _perfectPlacementsLevel1 + 1, transform);
                        NextBlock();
                    }
                    else // level 3
                    {
                        if (_topBlock.Bounds.size == _blockConfig.BlockStartingSize)
                        {
                            _perfectPlacementCount = 0;
                            _topBlock = _blockFactory.GenerateStaticBlockReward(ref newTopBounds, _perfectPlacementsLevel2, transform);
                            NextBlock();
                        }
                        else
                        {
                            _topBlock = _blockFactory.GenerateGrowingBlockPulseHalo(ref newTopBounds, _blockConfig.BlockStartingSize, _perfectPlacementsLevel2, NextBlock, transform);
                        }
                    }
                }
                else
                {
                    // The block was not perfectly placed onto the stack so the block is chopped in two.
                    _invokeEvents.BlockCut.Invoke();
                    // Reset perfect placement count.
                    _perfectPlacementCount = 0;
                    // Block is cut in two by spawning two new blocks.
                    Bounds overlapBounds = BlockUtils.CalculateOverlapBounds(ref movingBlockBounds, ref topBlockBounds, _movementAxis);
                    Bounds overhangBounds = BlockUtils.CalculateOverhangBounds(ref movingBlockBounds, ref topBlockBounds, _movementAxis);
                    _blockFactory.GenerateRigidbodyBlock(ref overhangBounds, transform);
                    _topBlock = _blockFactory.GenerateStaticBlock(ref overlapBounds, transform);
                    NextBlock();
                }
                _invokeEvents.BlockPlaced.Invoke();
            }
            else
            {
                // The block was not placed on the stack. 
                _blockFactory.GenerateRigidbodyBlock(ref movingBlockBounds, transform);
                _invokeEvents.BlockMissed.Invoke();
            }
        }

        void NextBlock()
        {
            _movementAxis = BlockUtils.SwapXZAxis(_movementAxis);
            Bounds topBlockBounds = _topBlock.Bounds;
            Bounds movingBounds = BlockUtils.CalculateAboveBounds(ref topBlockBounds);
            Vector3 spawnOffset = GetSpawnOffset(_movementAxis);
            _movingBlock = _blockFactory.GeneratePingPongBlock(ref movingBounds, spawnOffset + movingBounds.center, movingBounds.center - spawnOffset, transform, true);
        }

        void GenerateInitialStack()
        {
            Bounds blockBounds = new Bounds(Vector3.zero, _blockConfig.BlockStartingSize);
            for (int i = 0; i < _stackConfig.StartingStackHeight; ++i)
            {
                _topBlock = _blockFactory.GenerateStaticBlock(ref blockBounds, transform);
                blockBounds = BlockUtils.CalculateAboveBounds(ref blockBounds);
            }
        }

        Vector3 GetSpawnOffset(XZAxis axis)
        {
            return (axis == XZAxis.Z_AXIS ? Vector3.forward : Vector3.left) * _stackConfig.SpawnDistanceFromStack;
        }
    }
}
