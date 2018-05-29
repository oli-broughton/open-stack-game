using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OB
{
    [CreateAssetMenu(menuName = "Data/RandomAudio")]
    public class RandomAudio : ScriptableObject
    {
        [SerializeField]
        AudioClip m_audioClip;

        [SerializeField, MinMaxRange(0.1f, 3.0f)]
        RangedFloat m_pitch;

        [SerializeField, MinMaxRange(0.1f, 1.5f)]
        RangedFloat m_volume;

        public void Play(AudioSource audioSource)
        {
            audioSource.volume = Random.Range(m_pitch.minValue, m_pitch.maxValue);
            audioSource.pitch = Random.Range(m_pitch.minValue, m_pitch.maxValue);
            audioSource.PlayOneShot(m_audioClip);
        }
    }
}
