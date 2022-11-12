using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAudioController : MonoBehaviour
{

    public AudioSource sceneAudioSource;

    public AudioClip clickClip;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GetComponent<AudioSource>())
        {
            sceneAudioSource =  GetComponent<AudioSource>();
            AudioManager.sceneAudioSource = sceneAudioSource;
        }
        else
        {
            AudioManager.sceneAudioSource = this.gameObject.AddComponent<AudioSource>();
            sceneAudioSource = AudioManager.sceneAudioSource;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
