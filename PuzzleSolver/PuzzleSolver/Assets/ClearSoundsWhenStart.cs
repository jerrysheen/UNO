using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSoundsWhenStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.sceneAudioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
