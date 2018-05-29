using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OB.Events
{
    
    [CreateAssetMenu(menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        UnityEvent _unityEvent = new UnityEvent();

        public void AddListener(UnityAction listener)
        {
            _unityEvent.AddListener(listener);
        }

        public void RemoveListener(UnityAction listener)
        {
            _unityEvent.RemoveListener(listener);
        }

        public void Invoke()
        {
            _unityEvent.Invoke();
        }

        void OnDisable()
        {
            _unityEvent.RemoveAllListeners();
        }
    }
}