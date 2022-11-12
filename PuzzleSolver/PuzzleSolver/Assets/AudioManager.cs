using System.Collections;
using System.Collections.Generic;
using SystemManager;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
    public static AudioSource BgmaudioSource;
    public static AudioSource sceneAudioSource;
    public static AudioSource sceneClickSource;
        protected override void Awake()
        {
            base.Awake();
        }

        public void PlaySceneAudio(AudioClip clip, float volume, bool loop, float delay)
        {
            if (sceneAudioSource)
            {
                Debug.Log("buttonDown");
                AudioManager.sceneAudioSource.clip = clip;
                AudioManager.sceneAudioSource.loop = loop;
                AudioManager.sceneAudioSource.volume = volume;
          
                AudioManager.sceneAudioSource.Play((ulong)delay);
            }
            else
            {
                Debug.Log("Scene Audio Null");
            }
        }

        public void StopPlaySceneAudio(AudioClip clip)
        {
            if (sceneAudioSource && clip.name == sceneAudioSource.clip.name)
            {
                AudioManager.sceneAudioSource.Stop();
            }
        }
}
