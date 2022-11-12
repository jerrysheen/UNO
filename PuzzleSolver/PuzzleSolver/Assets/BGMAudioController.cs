using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudioController : MonoBehaviour
{
    public bool needReset = false;
    public AudioSource bgmAudioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (needReset)
        {
            if (GetComponent<AudioSource>())
            {
                bgmAudioSource =  GetComponent<AudioSource>();
                AudioManager.BgmaudioSource = bgmAudioSource;
            }
            else
            {
                AudioManager.BgmaudioSource = this.gameObject.AddComponent<AudioSource>();
                bgmAudioSource = AudioManager.BgmaudioSource;
            }
        }
        else
        {
            bgmAudioSource =  GetComponent<AudioSource>();
            bgmAudioSource = AudioManager.BgmaudioSource;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
