using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace OB.Events
{
    public abstract class GameEvent1Arg<T>: ScriptableObject
    {
        class GameEvent1ArgEvent : UnityEvent<T> { }

        GameEvent1ArgEvent m_event = new GameEvent1ArgEvent();

        public void AddListener(UnityAction<T> listener)
        {
            m_event.AddListener(listener);
        }

        public void RemoveListener(UnityAction<T> listener)
        {
            m_event.RemoveListener(listener);
        }

        public void Invoke(T arg1)
        {
            m_event.Invoke(arg1);
        }

        void OnDisable()
        {
            m_event.RemoveAllListeners();
        }
    }
}