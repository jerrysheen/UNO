using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine07
    {
        Start = 0,
        Click_Lock = 1,
        Lock_Shink = 2,
        Move_Carpet = 3,
        Click_Key = 4,
        Show_dialogue = 5,
        Move_Chair = 6,
        Get_CatFood = 7,
        Get_Key = 8,
        Get_Money = 9,
        Finished = 10,
    }

    public class StoryScene07 : Story
    {
        public StoryLine07 currState = StoryLine07.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene07>();
                if (!storyManager.currStory)
                {
                    storyManager.currStory = this;
                }
            }
            currState = StoryLine07.Start;
            TotalStoryLineNum = StoryLine07.GetNames(typeof(StoryLine07)).Length;
            StoryManager.getInstance.ValiDateState((int)currState);
            StoryManager.onGameStateChanged += onGameStateChange;
            
            
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine07) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

        
        
    }
}
