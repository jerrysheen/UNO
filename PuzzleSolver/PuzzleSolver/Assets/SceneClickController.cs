using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneClickController : MonoBehaviour
{

    public AudioSource sceneClickSource;

    public AudioClip clickClip;
    public float clickVolumn = 0.4f;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GetComponent<AudioSource>())
        {
            sceneClickSource =  GetComponent<AudioSource>();
            AudioManager.sceneClickSource = sceneClickSource;
        }
        else
        {
            AudioManager.sceneClickSource = this.gameObject.AddComponent<AudioSource>();
            sceneClickSource = AudioManager.sceneClickSource;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("buttonDown");
            AudioManager.sceneClickSource.clip = clickClip;
            AudioManager.sceneClickSource.loop = false;
            AudioManager.sceneClickSource.volume = clickVolumn;
          
            AudioManager.sceneClickSource.Play((ulong)0.0f);
        }

    }
}
