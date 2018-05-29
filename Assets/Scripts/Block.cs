using System.Collections;
using System.Collections.Generic;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// GameObject repesenting a block.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(BoxCollider))]
    public class Block : MonoBehaviour
    {
        BoxCollider _boxCollider;
        MeshRenderer _meshRenderer;
        MeshFilter _meshFilter;

        public Bounds Bounds { get { return _boxCollider.bounds; } set {_boxCollider.center = value.center; _boxCollider.size = value.size;} }

        void Awake()
        {
            _boxCollider = this.GetComponentAssert<BoxCollider>();
            _meshRenderer = this.GetComponentAssert<MeshRenderer>();
            _meshFilter = this.GetComponentAssert<MeshFilter>();
        }

        public void SetSize(Vector3 size)
        {
            Mesh boxMesh = MeshUtils.GenerateBlockMesh(size);
            _meshFilter.sharedMesh = boxMesh;
            _boxCollider.size = size;
        }

        public void SetColour(Color colour)
        {
            _meshRenderer.material.color = colour;
        }
    }
}
