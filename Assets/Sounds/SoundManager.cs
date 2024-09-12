using System.Collections;
using System.Collections.Generic;
using ProjectTools;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        public SerializableDictionary<SoundType, AudioClip> Sounds = new();
        public List<SoundType> SoundsToFadeIn = new();
        public List<SoundType> SoundsToFadeOut = new();
        public List<SoundType> SoundsToLoop = new();
        
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
            PlaySoundType(SoundType.PlaneNoise);
        }

        public void PlaySoundType(SoundType soundType)
        {
             PlaySound(Sounds[soundType], SoundsToLoop.Contains(soundType), SoundsToFadeIn.Contains(soundType));
        }

        public void StopSoundType(SoundType soundType)
        {
            foreach(AudioSource audioSource in _audioSources)
            {
                if(audioSource.clip == Sounds[soundType])
                {
                    if (SoundsToFadeOut.Contains(soundType))
                    {
                        StartCoroutine(FadeOut(0, 1, audioSource));                        
                    }
                    else
                    {
                        audioSource.Stop();
                    }
                }
            }
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            return audioSource;
        }

        private void PlaySound(AudioClip clip, bool loop = false, bool fadeIn = false)
        {
            AudioSource audioSource = GetFreeAudioSource();
            audioSource.clip = clip;
            audioSource.loop = loop;
            if(fadeIn)
            {
                StartCoroutine(FadeIn(audioSource, 1));
            }else audioSource.Play();
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
        
        IEnumerator FadeOut(float delay, float duration, AudioSource audioSource) 
        {
            yield return new WaitForSeconds(delay);
            float timeElapsed = 0;
            while (audioSource.volume > 0)
            {
                audioSource.volume = Mathf.Lerp(1, 0, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            audioSource.Stop();
        }

        IEnumerator FadeIn(AudioSource audioSource, float duration)
        {
            float timeElapsed = 0;
            audioSource.volume = 0;
            audioSource.Play();
            while (audioSource.volume < 1)
            {
                audioSource.volume = Mathf.Lerp(0, 1, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    public enum SoundType
    {
        PlaneNoise,
        PlaneDing,
        AlertCrash,
    }
}
