using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// 现在所在某个场景，就要执行这个line所对应的事情， 比方说drag cloth对应期待你把衣服收好。
    /// </summary>
    public enum StoryLine03
    {
        Start = 0,
        Click_Door = 1,
        PopUp_Dialogue = 2,
        Drag_HeadDress = 3,
        Drag_Cloth = 4,
        FinishThisTwo = 5,
        TranstoNext = 6,
        
    }

    public class StoryScene03 : Story
    {
        public StoryLine03 currState = StoryLine03.Start;

        private void Start()
        {
            var storyManager = StoryManager.getInstance;
            if (storyManager == null)
            {
                Debug.LogError("Story Manager not init yet");
            }
            else
            {
                storyManager.AddNewStory<StoryScene03>();
                 if (!storyManager.currStory || storyManager.currStory.ToString() != this.ToString())
                 {
                    storyManager.currStory = this;
                 }
            }
            currState = StoryLine03.Start;
            TotalStoryLineNum = StoryLine03.GetNames(typeof(StoryLine03)).Length;
            taskToDo = new List<int>();
            for (int i = 0; i < TotalStoryLineNum; i++)
            {
                taskToDo.Add(0);
            }

            //storyManager.currStory.taskToDo = taskToDo;
            StoryManager.getInstance.ValiDateState((int)0);
            StoryManager.getInstance.ValiDateState((int)1);
            StoryManager.onGameStateChanged += onGameStateChange;
            
            
        }
        
        private void onGameStateChange(int obj)
        {
            currState = (StoryLine03) obj;
        }

        private void OnDestroy()
        {
            StoryManager.onGameStateChanged -= onGameStateChange;
        }

        
        
    }
}
