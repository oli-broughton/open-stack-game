using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB.Game
{
    public static class BlockUtils
    {
        /// <summary>
        /// Check to see if two block bounds vertically overlap.
        /// </summary>
        public static bool Overlap(ref Bounds above, ref Bounds below, float ySkin = 0.1f)
        {
            Bounds aboveExpanded = above;
            if (ySkin > 0)
            {
                aboveExpanded.Expand(new Vector3(0, ySkin * aboveExpanded.size.y * ySkin));
            }
            return above.Intersects(below);
        }

        /// <summary>
        /// Check to see if both bounds vertically overlap perfectly along the XZ plane.
        /// </summary>
        /// <returns><c>true</c>The provided bounds perfectly overlap<c>false</c>The provided bounds do not perfectly overlap.</returns>
        public static bool PerfectOverlap(ref Bounds above, ref Bounds below, float errorMargin)
        {

            float xDiff = Mathf.Abs(above.max.x - below.max.x);
            float zDiff = Mathf.Abs(above.max.z - below.max.z);
            float xErrorRatio = above.size.x * errorMargin;
            float zErrorRatio = above.size.z * errorMargin;

            return (xDiff <= xErrorRatio && zDiff <= zErrorRatio);
        }

        /// <summary>
        /// Calculates the XZ overlap region of the provided bounds along a specific axis.
        /// </summary>
        public static Bounds CalculateOverlapBounds(ref Bounds above, ref Bounds below, XZAxis axis)
        {
            Bounds overlapBounds = new Bounds();
            float minX = above.min.x, maxX = above.max.x;
            float minZ = above.min.z, maxZ = above.max.z;

            if (axis == XZAxis.X_AXIS)
            {
                if (Mathf.Sign(above.max.x - below.max.x) > 0)
                {
                    minX = above.min.x;
                    maxX = below.max.x;
                }
                else
                {
                    minX = below.min.x;
                    maxX = above.max.x;
                }
            }
            else if (axis == XZAxis.Z_AXIS)
            {
                if (Mathf.Sign(above.max.z - below.max.z) > 0)
                {
                    minZ = above.min.z;
                    maxZ = below.max.z;
                }
                else
                {
                    minZ = below.min.z;
                    maxZ = above.max.z;
                }
            }

            overlapBounds.SetMinMax(new Vector3(minX, above.min.y, minZ),
                     new Vector3(maxX, above.max.y, maxZ));

            return overlapBounds;
        }

        /// <summary>
        /// Calculates the XZ overhang (opposite to overlap) region of the provided bounds along a specific axis.
        /// </summary>
        public static Bounds CalculateOverhangBounds(ref Bounds above, ref Bounds below, XZAxis axis)
        {
            Bounds diffBounds = new Bounds();
            float minX = above.min.x, maxX = above.max.x;
            float minZ = above.min.z, maxZ = above.max.z;

            if (axis == XZAxis.X_AXIS)
            {
                if (Mathf.Sign(above.max.x - below.max.x) > 0)
                {
                    minX = below.max.x;
                    maxX = above.max.x;
                }
                else
                {
                    minX = above.min.x;
                    maxX = below.min.x;
                }
            }
            else if (axis == XZAxis.Z_AXIS)
            {
                if (Mathf.Sign(above.max.z - below.max.z) > 0)
                {
                    minZ = below.max.z;
                    maxZ = above.max.z;
                }
                else
                {
                    minZ = above.min.z;
                    maxZ = below.min.z;
                }
            }

            diffBounds.SetMinMax(new Vector3(minX, above.min.y, minZ),
                                 new Vector3(maxX, above.max.y, maxZ));

            return diffBounds;
        }

        /// <summary>
        /// Calculates the bounds directly above the provided bounds.
        /// </summary>
        /// <returns>The above bounds.</returns>
        /// <param name="below">The below bounds.</param>
        public static Bounds CalculateAboveBounds(ref Bounds below)
        {
            Bounds above = below;
            above.center += Vector3.up * below.size.y;
            return above;
        }

        /// <summary>
        /// Swaps the provided axis.
        /// </summary>
        /// <returns>The swapped axis</returns>
        /// <param name="axis">Current Axis</param>
        public static XZAxis SwapXZAxis(XZAxis axis)
        {
            return (axis == XZAxis.X_AXIS ? XZAxis.Z_AXIS : XZAxis.X_AXIS);
        }
    }

    public static class MeshUtils
    {
        /// <summary>
        /// Generates vertices for a block. 
        /// </summary>
        /// <returns>The block vertices.</returns>
        /// <param name="size">The size of the block. Centered at the origin. </param>
        static Vector3[] GenerateBlockVertices(Vector3 size)
        {
            Vector3 boxExtents = size * 0.5f;
            // box vertices 
            Vector3 boxV1 = new Vector3(-boxExtents.x, boxExtents.y, -boxExtents.z);
            Vector3 boxV2 = new Vector3(boxExtents.x, boxExtents.y, -boxExtents.z);
            Vector3 boxV3 = new Vector3(boxExtents.x, -boxExtents.y, -boxExtents.z);
            Vector3 boxV4 = new Vector3(-boxExtents.x, -boxExtents.y, -boxExtents.z);

            Vector3 boxV5 = new Vector3(-boxExtents.x, boxExtents.y, boxExtents.z);
            Vector3 boxV6 = new Vector3(boxExtents.x, boxExtents.y, boxExtents.z);
            Vector3 boxV7 = new Vector3(boxExtents.x, -boxExtents.y, boxExtents.z);
            Vector3 boxV8 = new Vector3(-boxExtents.x, -boxExtents.y, boxExtents.z);

            // triangle vertices
            Vector3[] vertices =
            {
            // front
            boxV1, boxV2, boxV4,
            boxV4, boxV2, boxV3,

            // back
            boxV6, boxV5, boxV7,
            boxV7, boxV5, boxV8,

            // top
            boxV5, boxV6, boxV1,
            boxV1, boxV6, boxV2,

            // bottom
            boxV7, boxV8, boxV3,
            boxV3, boxV8, boxV4,

            // right 
            boxV2, boxV6, boxV3,
            boxV3, boxV6, boxV7,

            // left 
            boxV5, boxV1, boxV8,
            boxV8, boxV1, boxV4
            };
            return vertices;
        }

        /// <summary>
        /// Recalculate the size of the provided block mesh.
        /// </summary>
        public static void UpdateBlockMeshSize(Mesh mesh, Vector3 size)
        {
            mesh.vertices = GenerateBlockVertices(size);
            mesh.RecalculateBounds();
        }

        /// <summary>
        /// Generates a complete block mesh.
        /// </summary>
        public static Mesh GenerateBlockMesh(Vector3 size)
        {

            Mesh boxMesh = new Mesh();

            Vector3[] vertices = GenerateBlockVertices(size);

            Vector3 frontN = Vector3.back;
            Vector3 backN = Vector3.forward;
            Vector3 topN = Vector3.up;
            Vector3 bottomN = Vector3.down;
            Vector3 leftN = Vector3.left;
            Vector3 rightN = Vector3.right;

            // triangle normals
            Vector3[] normals =
            {
            // front
            frontN, frontN, frontN,
            frontN, frontN, frontN,

            // back
            backN, backN, backN,
            backN, backN, backN,

            // top
            topN, topN, topN,
            topN, topN, topN,

            // bottom
            bottomN, bottomN, bottomN,
            bottomN, bottomN, bottomN,

            // right
            rightN, rightN, rightN,
            rightN, rightN, rightN,

            // left
            leftN, leftN, leftN,
            leftN, leftN, leftN
        };

            // triangle indices
            int[] indices = new int[vertices.Length];
            for (int i = 0; i < indices.Length; ++i)
                indices[i] = i;

            boxMesh.vertices = vertices;
            boxMesh.normals = normals;
            boxMesh.SetIndices(indices, MeshTopology.Triangles, 0);
            boxMesh.RecalculateBounds();
            return boxMesh;
        }

        /// <summary>
        /// Generates a halo mesh on the XZ plane.
        /// </summary>
        /// <returns>The block halo mesh.</returns>
        public static Mesh GenerateBlockHaloMesh(Vector3 blockSize, float haloThickness)
        {
            // halo mesh is made from 4 quads that surround the given block size. 
            Mesh haloMesh = new Mesh();
            Vector3 haloExtents = 0.5f * blockSize;

            // shared vertices form 3 quads
            // inner quad
            Vector3 innerv1 = new Vector3(-haloExtents.x, 0, -haloExtents.z);
            Vector3 innerv2 = new Vector3(haloExtents.x, 0, -haloExtents.z);
            Vector3 innerv3 = new Vector3(haloExtents.x, 0, haloExtents.z);
            Vector3 innerv4 = new Vector3(-haloExtents.x, 0, haloExtents.z);
            // outer x scale quad
            Vector3 xquadv1 = new Vector3(-haloExtents.x + -haloThickness, 0, -haloExtents.z);
            Vector3 xquadv2 = new Vector3(haloExtents.x + haloThickness, 0, -haloExtents.z);
            Vector3 xquadv3 = new Vector3(haloExtents.x + haloThickness, 0, haloExtents.z);
            Vector3 xquadv4 = new Vector3(-haloExtents.x + -haloThickness, 0, haloExtents.z);
            // outer z scale quad
            Vector3 zquadv1 = new Vector3(-haloExtents.x + -haloThickness, 0, -haloExtents.z + -haloThickness);
            Vector3 zquadv2 = new Vector3(haloExtents.x + haloThickness, 0, -haloExtents.z + -haloThickness);
            Vector3 zquadv3 = new Vector3(haloExtents.x + haloThickness, 0, haloExtents.z + haloThickness);
            Vector3 zquadv4 = new Vector3(-haloExtents.x + -haloThickness, 0, haloExtents.z + haloThickness);


            Vector3[] vertices =
            {
                 innerv1,
                 innerv2,
                 innerv3,
                 innerv4,
                 xquadv1,
                 xquadv2,
                 xquadv3,
                 xquadv4,
                 zquadv1,
                 zquadv2,
                 zquadv3,
                 zquadv4
            };

            int[] indices =
            {
                // front quad
                8, 4, 5,
                5, 9, 8,

               // back quad
               7, 11, 10,
               10, 6, 7,

               // left quad
               4, 7, 3,
               3, 0, 4,

               // right quad
               2, 6, 5,
               5, 1, 2
            };

            Vector3[] normals = ArrayUtils.InitialiseFill(vertices.Length, Vector3.up);


            haloMesh.vertices = vertices;
            haloMesh.normals = normals;
            haloMesh.SetIndices(indices, MeshTopology.Triangles, 0);
            haloMesh.RecalculateBounds();
            return haloMesh;
        }
    }

    public static class ArrayUtils
    {
        /// <summary>
        /// Creates and initialises an array with constucted objects.
        /// </summary>
        /// <returns>The initialised array.</returns>
        /// <param name="length">The length of the array.</param>
        /// <typeparam name="T">The type of the array.</typeparam>
        public static T[] Initialise<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
                array[i] = new T();
            return array;
        }

        /// <summary>
        /// Creates and fills an array with a provided value.
        /// </summary>
        /// <returns>The initialised array.</returns>
        /// <param name="length">The length of the array.</param>
        /// <param name="value">Value to fill the array with.</param>
        /// <typeparam name="T">The type of the array.</typeparam>
        public static T[] InitialiseFill<T>(int length, T value) where T : struct
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
                array[i] = value;
            return array;
        }
    }
}