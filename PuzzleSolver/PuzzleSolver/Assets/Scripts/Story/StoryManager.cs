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
        public Dictionary<string, Story> storyContainer;

        protected override void Awake()
        {
            base.Awake();
            storyContainer = new Dictionary<string, Story>();
        }

        public void AddNewStory<T>() where T : Story
        {
            var storyName = typeof(T).ToString();
            
            if (storyContainer.TryGetValue(storyName, out Story m_Story))
            {
            }
            else
            {
                storyContainer[storyName] = m_Story;
            }
        }

        public T getStory<T>() where T : Story
        {
            var storyName = typeof(T).ToString();
            
            storyContainer.TryGetValue(storyName, out Story m_Story);
            return (T)m_Story;
        }
    } 
    public abstract class Story : MonoBehaviour
    {
        public int currStoryLine;
        public string nextSceneName;
        public virtual void GoToNext(int curr, int next)
        {
            if (curr == currStoryLine) currStoryLine = next;
        }

    }
}
