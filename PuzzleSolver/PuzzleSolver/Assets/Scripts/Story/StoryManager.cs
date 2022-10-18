using System.Collections;
using System.Collections.Generic;
using SystemManager;
using PuzzleSolver;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StoryManagement
{
    public class StoryManager :  SingletonMono<StoryManager>
    {
        public Dictionary<string, int> StoryContainer;

        protected override void Awake()
        {
            base.Awake();
            StoryContainer = new Dictionary<string, int>();
        }
    } 
    public abstract class Story
    {
        public int currStoryLine;
        public string nextSceneName;
        public virtual void GoToNext(int curr, int next)
        {
            if (curr == currStoryLine) currStoryLine = next;
        }

    }
}
