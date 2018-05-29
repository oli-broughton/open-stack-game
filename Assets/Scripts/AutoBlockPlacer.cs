using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Automatically places perfect blocks on the stack.
    /// * Will disable itself when not in editor mode *
    /// * Intended for debug purposes only *
    /// </summary>
    [RequireComponent(typeof(Stack))]
    public class AutoBlockPlacer : MonoBehaviour
    {
        [SerializeField] bool _autoPlay;

        Stack _stack;

        void Awake()
        {
#if ! UNITY_EDITOR
            enabled = false;
#else
            enabled = true;
#endif
            _stack = GetComponent<Stack>();
        }

        void Update()
        {
            if (_autoPlay && _stack.IsPerfectOverlap())
            {
                _stack.PlaceBlock();
            }
        }
    }
}
