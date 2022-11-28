using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene04Click : UIControllBase
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

    public override void OnClicked()
    {
        base.OnClicked();
        if (StoryManager.getInstance.currStory.currStoryLine == reactToIndex)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (StoryManager.getInstance.currStory.currStoryLine == reactToIndex)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
    }
}
