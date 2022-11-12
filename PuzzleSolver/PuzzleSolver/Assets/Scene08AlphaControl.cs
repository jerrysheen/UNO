using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.UI;

public class Scene08AlphaControl : UIControllBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public void onGameStateChange(int state)
    {
        if (state == reactToLine)
        {
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            var img = GetComponent<Image>();
            if (img != null)
            {
                Color curr = img.color;
                curr.a = 233.0f / 255.0f;
                img.color = curr;
            }

            foreach (var VARIABLE in GetComponentsInChildren<Image>())
            {
                Color curr = VARIABLE.color;
                curr.a = 150.0f / 255.0f;
                VARIABLE.color = curr;
            }
        }

    }
}

