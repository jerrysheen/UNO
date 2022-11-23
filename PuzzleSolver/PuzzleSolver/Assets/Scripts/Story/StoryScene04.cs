using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine04
    {
        Start = 0,
        ClickCage00 = 1,
        ClickCage01 = 2,
        MoveStep = 3,
        StepMoveing = 4,
        CharecterMoving = 5,
        PlayDialogue = 6,
        ButterFly_Flyway = 7,
        Charecter_Move = 8,
        Cage_disapear = 9,
        Butterfly_Fly = 10,
        Butterfl = 11,
        Butterfly = 12,
        End_Scene = 13
        
    }

    public class StoryScene04 : Story
    {
        public StoryLine04 currState = StoryLine04.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene04>();
                if (!storyManager.currStory)
                {
                    storyManager.currStory = this;
                }
            }
            currState = StoryLine04.Start;
            TotalStoryLineNum = StoryLine04.GetNames(typeof(StoryLine04)).Length;
            StoryManager.getInstance.ValiDateState((int)currState);
            StoryManager.onGameStateChanged += onGameStateChange;
            
            
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine04) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

        
        
    }
}
