using OB.Events;
using OB.Extensions;
using UnityEngine;

namespace OB.Game
{
    /// <summary>
    /// Plays the provided random audio when a block cut event is called
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class RandomAudioEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent _event;
        [SerializeField] RandomAudio _randomAudio;

        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = this.GetComponentAssert<AudioSource>();
        }

		void OnEnable()
		{
            _event.AddListener(PlayRandomAudio);
		}

		void OnDisable()
		{
            _event.RemoveListener(PlayRandomAudio);
		}

        void PlayRandomAudio()
        {
            _randomAudio.Play(_audioSource);
        }
    }
}