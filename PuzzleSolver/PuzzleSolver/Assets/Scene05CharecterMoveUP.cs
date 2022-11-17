using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene05CharecterMoveUP : UIControllBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChanged;
    }

    public void onGameStateChanged(int state)
    {
        if (state == reactToLine)
        {
            // play music , + move 
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
        }
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChanged;
    }

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
