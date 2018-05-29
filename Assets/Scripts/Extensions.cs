using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


namespace OB.Extensions
{
    static class ArrayExtensions
    {
        /// <summary>
        /// Shuffle the specified array. O(n).
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        /// <typeparam name="T">The array type.</typeparam>
        public static void Shuffle<T>(this T[] array)
        {
            // https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
            // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle

            int n = array.Length;
            while (n > 1)
            {
                int k = UnityEngine.Random.Range(0, n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }

    static class GameObjectExtensions
    {
        /// <summary>
        /// GetComponent but will fail an assert if null.
        /// </summary>
        /// <returns>The component if found, null otherwise.</returns>
        /// <param name="go">The game object.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentAssert<T>(this GameObject go) where T : Component
        {
            T returComp = go.GetComponent<T>();
            Assert.IsNotNull<T>(returComp, "No component of type " + typeof(T) + " attached to " + go.name);
            return returComp;
        }

        /// <summary>
        /// GetComponentInChildren but will fail an assert if null.
        /// </summary>
        /// <returns>The component if found, null otherwise.</returns>
        /// <param name="go">The game object.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentInChildrenAssert<T>(this GameObject go) where T : Component
        {
            T returComp = go.GetComponentInChildren<T>();
            Assert.IsNotNull<T>(returComp, "No component of type " + typeof(T) + " attached to " + go.name + " or its children");
            return returComp;
        }

        /// <summary>
        /// If no component is found it will be added to the game object.
        /// </summary>
        /// <returns>The component.</returns>
        /// <param name="go">The game object.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentSafe<T>(this GameObject go) where T : Component
        {
            T comp = go.GetComponent<T>();
            if (comp == null)
                comp = go.AddComponent<T>();
            return comp;
        }
    }

    static class ComponentExtensions
    {
        /// <summary>
        /// GetComponent but will fail an assert if null.
        /// </summary>
        /// <returns>The component if found, null otherwise.</returns>
        /// <param name="comp">The component.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentAssert<T>(this Component comp) where T : Component
        {
            return comp.gameObject.GetComponentAssert<T>();
        }

        /// <summary>
        /// GetComponentInChildren but will fail an assert if null.
        /// </summary>
        /// <returns>The component if found, null otherwise.</returns>
        /// <param name="comp">The component.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentInChildrenAssert<T>(this Component comp) where T : Component
        {
            return comp.gameObject.GetComponentInChildrenAssert<T>();
        }

        /// <summary>
        /// If no component is found it will be added to the components game object.
        /// </summary>
        /// <returns>The component safe.</returns>
        /// <param name="comp">The component.</param>
        /// <typeparam name="T">Component Type.</typeparam>
        public static T GetComponentSafe<T>(this Component comp) where T : Component
        {
            return comp.gameObject.GetComponentSafe<T>();
        }
    }


}