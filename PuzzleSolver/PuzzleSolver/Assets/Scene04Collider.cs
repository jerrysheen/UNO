using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene04Collider : MonoBehaviour
{
    public int reactToIndex = -1;

    public int nextLineIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (StoryManager.getInstance.currStory.currStoryLine == reactToIndex)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
    }
}
