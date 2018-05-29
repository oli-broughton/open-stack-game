using OB.Events;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Plays the provided audio scale as blocks are perfectly placed onto the stack.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class PerfectPlacementAudio : MonoBehaviour
    {
        [System.Serializable]
        public class ResponseEvents
        {
            public GameEventInt PlayNextNote;
        }

        [System.Serializable]
        public class PerfectPlacementNotes
        {
            public AudioClip[] scale;
        }

        [SerializeField] ResponseEvents _responseEvents;
        [SerializeField] PerfectPlacementNotes _perfectPlacementNotes = null;
        [SerializeField] AudioClip _scaleComplete = null;

        AudioSource _audioSource;

        void Awake()
        {   
            _audioSource = this.GetComponentAssert<AudioSource>();
        }

        void OnEnable()
        {
            _responseEvents.PlayNextNote.AddListener(PlayNextScaleNote);
        }

        void OnDisable()
        {
            _responseEvents.PlayNextNote.RemoveListener(PlayNextScaleNote);
        }

        void PlayNextScaleNote(int scaleIndex)
        {
            int clampedScale = Mathf.Clamp(scaleIndex, 0, _perfectPlacementNotes.scale.Length);
            if (clampedScale < _perfectPlacementNotes.scale.Length)
            {
                _audioSource.PlayOneShot(_perfectPlacementNotes.scale[clampedScale]);
            }
            else
            {
                _audioSource.PlayOneShot(_scaleComplete);
            }
        }
    }
}