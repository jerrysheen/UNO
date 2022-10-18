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
        public override void GoToNext(int curr, int next)
        {
            base.GoToNext(curr, next);
        }

        private void Start()
        {
            var storyManager = StoryManagement.StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene01>();
            }

            currState = StoryLine01.Start;
        }
    }
    
}
