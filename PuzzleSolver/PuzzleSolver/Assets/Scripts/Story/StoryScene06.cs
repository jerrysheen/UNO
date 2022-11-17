using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine06
    {
        Start = 0,
        Rotate = 1,
        Sign = 2,
        Dialogue = 3
    }

    public class StoryScene06 : Story
    {
        public StoryLine06 currState = StoryLine06.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene06>();
                if (!storyManager.currStory)
                {
                    storyManager.currStory = this;
                }
            }
            currState = StoryLine06.Start;
            TotalStoryLineNum = StoryLine06.GetNames(typeof(StoryLine06)).Length;
            StoryManager.getInstance.ValiDateState((int)currState);
            StoryManager.onGameStateChanged += onGameStateChange;
            
            
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine06) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

        
        
    }
}
