using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine01
    {
        Start = 0,
        Click_Scroll = 1,
        Click_Cinnabar = 2,
        Leave_FingerPrint = 3
    }

    public class StoryScene01 : Story
    {
        public StoryLine01 currState = StoryLine01.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene01>();
                if (!storyManager.currStory)
                {
                    storyManager.currStory = this;
                }
            }
            currState = StoryLine01.Start;
            TotalStoryLineNum = StoryLine01.GetNames(typeof(StoryLine01)).Length;
            StoryManager.getInstance.ValiDateState((int)currState);
            StoryManager.onGameStateChanged += onGameStateChange;
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine01) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

    }
    
}
