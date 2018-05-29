using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace OB.Game
{
    /// <summary>
    /// Translates a game object back and forth from a specified start and end position. 
    /// Once started this behaviour will loop until stopped.
    /// </summary>
    public class PingPongInfinite : MonoBehaviour
    {
        [SerializeField] BlockConfig _blockConfig;

        Tweener _playingTween = null;

        public void Play(Vector3 start, Vector3 end)
        {
            if (_playingTween == null)
            {
                transform.position = start;
                _playingTween = transform.DOMove(end, _blockConfig.BlockPingPongLoopTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
            }
        }

        public void Stop()
        {
            if (_playingTween != null)
            {
                _playingTween.Pause();
                _playingTween = null;
            }
        }
    }
}