using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine08
    {
        Start = 0,
        GetRice = 1,
        GetFlour = 2,
        GetMix = 3,
        PutWoods = 4,
        MoveCandle = 5,
        Fire = 6,
        aaa = 7,
        ccc = 8,
        cccd = 9,
        rrrr = 10,
        qqqq = 11,
        ffff = 12,
        sss = 13,
        a = 14,
        w = 15,
    }

    public class StoryScene08 : Story
    {
        public StoryLine08 currState = StoryLine08.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene08>();
                if (!storyManager.currStory)
                {
                    storyManager.currStory = this;
                }
            }
            currState = StoryLine08.Start;
            TotalStoryLineNum = StoryLine08.GetNames(typeof(StoryLine08)).Length;
            StoryManager.getInstance.ValiDateState((int)currState);
            StoryManager.onGameStateChanged += onGameStateChange;
            
            
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine08) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

        
        
    }
}
