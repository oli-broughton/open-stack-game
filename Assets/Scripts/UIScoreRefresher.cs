using OB.Events;
using OB.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace OB.UI
{
    /// <summary>
    /// Refreshes the attached Text component when the score is updated.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class UIScoreRefresher : MonoBehaviour
    {
        [SerializeField] GameEventInt _updateScore;

        Text _scoreText;

        void Awake()
        {
            _scoreText = this.GetComponentAssert<Text>();
        }

		void OnEnable()
		{
            _updateScore.AddListener(RefreshScore);
		}
		void OnDisable()
        {
            _updateScore.RemoveListener(RefreshScore);
        }

        void RefreshScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
