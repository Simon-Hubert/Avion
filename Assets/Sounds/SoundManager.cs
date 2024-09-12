using System.Collections.Generic;
using ProjectTools;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        public SerializableDictionary<SoundType, AudioClip> Sounds = new();
        
        private List<AudioSource> _audioSources = new();

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        void Start()
        {
            PlaySoundType(SoundType.PlaneNoise, true);
        }

        public void PlaySoundType(SoundType soundType, bool loop = false)
        {
             PlaySound(Sounds[soundType], loop);
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            return audioSource;
        }

        private void PlaySound(AudioClip clip, bool loop = false)
        {
            AudioSource audioSource = GetFreeAudioSource();
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    
        private AudioSource GetFreeAudioSource(){
            foreach (AudioSource audioSource in _audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    return audioSource;
                }
            }
            return CreateAudioSource();
        }
    }

    public enum SoundType
    {
        PlaneNoise,
        PlaneDing,
        AlertCrash,
    }
}
