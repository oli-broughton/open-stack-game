using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OB.Events;

namespace OB.Game
{
    /// <summary>
    /// Stores the current game score. 
    /// The score is added to whenever a block is placed onto the stack.
    /// </summary>
    public class Score : MonoBehaviour
    {
     
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEvent IncreaseScore;
        }

        [System.Serializable]
        public class InvokeEvents
        {
            public GameEventInt ScoreChanged;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] InvokeEvents _invokeEvents;
        [SerializeField] DifficultyConfig _diffConfig;

        int _score = 0;

		void OnEnable()
		{
            _responseEvents.IncreaseScore.AddListener(IncrementScore);
		}

		void OnDisable()
		{
            _responseEvents.IncreaseScore.RemoveListener(IncrementScore);
		}

		void IncrementScore()
        {
            _score += _diffConfig.ScoreIncrementAmount;
            _invokeEvents.ScoreChanged.Invoke(_score);
        }
    }
}