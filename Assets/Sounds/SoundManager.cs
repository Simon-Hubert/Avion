using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectTools;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        public SerializableDictionary<SoundType, AudioClip[]> Sounds = new();
        public List<SoundType> SoundsToFadeIn = new();
        public List<SoundType> SoundsToFadeOut = new();
        public List<SoundType> SoundsToLoop = new();
        public SerializableDictionary<SoundType, float> Volume = new();
        
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
             PlaySound(GetRandomSoundFromType(soundType), SoundsToLoop.Contains(soundType), SoundsToFadeIn.Contains(soundType), Volume.ContainsKey(soundType) ? Volume[soundType] : 1);
        }

        public void StopSoundType(SoundType soundType)
        {
            foreach(AudioSource audioSource in _audioSources)
            {
                if(audioSource.clip == GetRandomSoundFromType(soundType))
                {
                    if (SoundsToFadeOut.Contains(soundType))
                    {
                        StartCoroutine(FadeOut(0, 1, audioSource));                        
                    }
                    else
                    {
                        if (audioSource.loop)
                        {
                            audioSource.loop = false;
                            StartCoroutine(FadeOut(0, 1, audioSource));
                        }else audioSource.Stop();
                    }
                }
            }
        }
        
        public void PlayIfNotPlaying(SoundType soundType)
        {
            bool isPlaying = false;
            foreach(AudioSource audioSource in _audioSources)
            {
                if(audioSource.clip == GetRandomSoundFromType(soundType))
                {
                    isPlaying = audioSource.isPlaying;
                }
            }
            if (!isPlaying)
            {
                PlaySoundType(soundType);
            }
        }

        private AudioSource CreateAudioSource()
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            return audioSource;
        }

        private void PlaySound(AudioClip clip, bool loop = false, bool fadeIn = false, float volume = 1)
        {
            AudioSource audioSource = GetFreeAudioSource();
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.volume = volume;
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

        private AudioClip GetRandomSoundFromType(SoundType soundType)
        {
            return Sounds[soundType][Random.Range(0, Sounds[soundType].Length)];
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
        PlaneHotess,
        AlertCrash,
        CrashExplode,
        WindshieldWiper,
        PlaneDepressure,
        LongIntervalBip,
        MediumIntervalBip,
        ShortIntervalBip,
        MiniGameFailure,
        MiniGameSuccess,
        SeagullScream
    }
}
