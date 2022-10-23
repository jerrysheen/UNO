using System;
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

        /// <summary>
        ///  这个地方不去管他是第几个场景， 因为默认每个ui脚本只会在自己这个场景里面运行。
        /// 过了这个场景就销毁
        /// </summary>
        public static event Action<int> onGameStateChanged;

        public Story currStory = null;
        
        /// <summary>
        /// totalStoryLine 会比enum多一个，因为会多一个start
        /// </summary>
        /// <param name="next"></param>
        public void ValiDateState(int curr)
        {
            //if (curr == currStoryLine) currStoryLine = next;

            if (currStory.currStoryLine == curr - 1 || currStory.currStoryLine == 0)
            {
                currStory.currStoryLine = curr;
            }
            if (currStory.TotalStoryLineNum == curr + 1)
            {
                StartCoroutine(Wait(3.0f));
                // story 根据场景注册，每次新场景加载完之后判断，如果是null的话就给新值。
                
            }
            
            onGameStateChanged?.Invoke(curr);
        }
        
        IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            GoToNextScene(currStory.nextSceneName);
            currStory = null;
        }

        public void GoToNextScene(string nextScene)
        {
            SceneManager.LoadScene(nextScene);
        }

        protected override void Awake()
        {
            base.Awake();
            currStory = null;
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
        public int TotalStoryLineNum;
        public string currSceneName;
        public string nextSceneName;
        public string currStoryClassName;
        public string nextStoryClassName;

    }
}
