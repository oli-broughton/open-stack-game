using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OB.Events;

namespace OB.Game
{
	/// <summary>
    /// Places blocks on the stack based on player input.
    /// </summary>
    [RequireComponent(typeof(Stack))]
    public class PlayerBlockPlacer : MonoBehaviour
    {
        [SerializeField] GameEvent _placeBlockInput;

		Stack _stack;

        void Awake()
        {
			_stack = GetComponent<Stack>();
        }

		 void OnEnable()
        {
            _placeBlockInput.AddListener(OnPlaceBlockInput);
        }

        void OnDisable()
        {
           _placeBlockInput.RemoveListener(OnPlaceBlockInput);
        }

		void OnPlaceBlockInput()
		{
			_stack.PlaceBlock();
		}
    }
}
