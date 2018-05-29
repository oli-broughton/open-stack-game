using System;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Responsible for generating the various block types for the game. 
    /// </summary>
    [RequireComponent(typeof(GradientPicker))]
    public class BlockFactory : MonoBehaviour
    {
        [System.Serializable]
        public class BlockTypes
        {
            public Block StaticBlockPrefab;
            public Block StaticBlockRewardPrefab;
            public Block PingPongBlock;
            public Block RigidbodyBlockPrefab;
            public Block GrowingBlockPrefab;
        }

        [System.Serializable]
        public class HaloTypes
        {
            public BlockHalo HaloFadePrefab;
            public BlockHalo HaloPulseFadePrefab;
        }

        [SerializeField] HaloConfig _haloConfig;

        [SerializeField] BlockTypes _blockPrefabs;

        [SerializeField] HaloTypes _haloPrefabs;

        GradientPicker _gradientPicker;

        Block _staticBlockCache;
        Block _pingPongBlockCache;
        
        void Awake()
        {
            _gradientPicker = this.GetComponentAssert<GradientPicker>();   
        }

         public Block GenerateStaticBlock(ref Bounds blockBounds, Transform parent = null, bool newColour = false)
        {
            Block block = InstantiateBlock(_blockPrefabs.StaticBlockPrefab, ref blockBounds, parent, newColour);
            return block;
        }

        public Block GenerateStaticBlockFadeHalo(ref Bounds blockBounds, Transform parent = null, bool newColour = false)
        {
            Block block = GenerateStaticBlock(ref blockBounds, parent, newColour);
            GenerateFadeHalo(ref blockBounds, block.transform);
            return block;
        }

        public Block GenerateStaticBlockPulseHalo(ref Bounds blockBounds, int noPulses, Transform parent = null, bool newColour = false)
        {
            Block block = InstantiateBlock(_blockPrefabs.StaticBlockPrefab, ref blockBounds, parent, newColour);
            GeneratePulseHalo(ref blockBounds, noPulses, block.transform);
            return block;
        }

        public Block GenerateStaticBlockReward(ref Bounds blockBounds, int noPulses, Transform parent = null, bool newColour = false)
        {
            Block block = InstantiateBlock(_blockPrefabs.StaticBlockRewardPrefab, ref blockBounds, parent, newColour);
            GeneratePulseHalo(ref blockBounds, noPulses, block.transform);
            return block;
        }

        public Block GeneratePingPongBlock(ref Bounds blockBounds, Vector3 startPos, Vector3 endPos, Transform parent = null, bool newColour = false)
        {
            Block pingPongBlock = InstantiateBlock(_blockPrefabs.PingPongBlock, ref blockBounds, parent, newColour);
            PingPongInfinite blockMove = pingPongBlock.GetComponentAssert<PingPongInfinite>();
            blockMove.Play(startPos, endPos);
            return pingPongBlock;
        }

        public Block GenerateRigidbodyBlock(ref Bounds blockBounds, Transform parent = null, bool newColour = false)
        {
            Block rigidbodyBlock = InstantiateBlock(_blockPrefabs.RigidbodyBlockPrefab, ref blockBounds, parent, newColour);
            return rigidbodyBlock;
        }

        public Block GenerateGrowingBlock(ref Bounds blockBounds, Vector3 maxSize, Action onGrowComplete = null, Transform parent = null, bool newColour = false)
        {
            Block block = InstantiateBlock(_blockPrefabs.GrowingBlockPrefab,ref blockBounds, parent, newColour);
            BlockGrow blockGrow = block.GetComponentAssert<BlockGrow>();
            blockGrow.Grow(maxSize, onGrowComplete);
            return block;
        }

        public Block GenerateGrowingBlockPulseHalo(ref Bounds blockBounds, Vector3 maxSize, int noPulses, Action onGrowComplete = null, Transform parent = null, bool newColour = false)
        {
            Block block = GenerateGrowingBlock(ref blockBounds, maxSize, onGrowComplete, parent, newColour);
            GeneratePulseHalo(ref blockBounds, noPulses, block.transform);
            return block;
        }

        void GenerateFadeHalo(ref Bounds blockBounds, Transform parent = null)
        {
            BlockHalo halo = InstantiateBlockHalo(_haloPrefabs.HaloFadePrefab,ref blockBounds, parent);
            halo.Play();
        }

        void GeneratePulseHalo(ref Bounds blockBounds, int noPulses, Transform parent = null)
        {
            for (int i = 0; i < noPulses; ++i)
            {
                BlockHalo halo = InstantiateBlockHalo(_haloPrefabs.HaloPulseFadePrefab,ref blockBounds, parent);
                halo.Play(_haloConfig.BlockHaloPulseDelaySeconds * i);
            }
        }

        Block InstantiateBlock(Block blockPrefab, ref Bounds blockBounds, Transform parent = null, bool newColour = false)
        {
            Block block = Instantiate(blockPrefab, parent, false);
            block.SetSize(blockBounds.size);
            block.transform.position = blockBounds.center;
            block.SetColour(((newColour) ? _gradientPicker.NextBlendedColour() : _gradientPicker.CurrentBlendedColour));
            return block;
        }

        BlockHalo InstantiateBlockHalo(BlockHalo blockHaloPrefab, ref Bounds blockBounds, Transform parent = null)
        {
            BlockHalo halo = Instantiate(blockHaloPrefab, parent, true);
            if(halo == null)
            {
                Debug.Log("TEST");
            }
            halo.SetSize(blockBounds);
            // set halo position to the bottom of provided block bounds (0.9f to prevent z fighting) 
            halo.transform.position = blockBounds.center + Vector3.down * blockBounds.extents.y * 0.9f;
            return halo;
        }
    }
}