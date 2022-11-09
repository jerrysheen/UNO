using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene08Manager : UIControllBase
{

    public int backToCurrentScene = -100;

    public string currSceneName = "";

    public int goToNextSceneIndex = -100;

    public string nextSceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public void onGameStateChange(int state)
    {
        if (state == backToCurrentScene)
        {
            SceneManager.LoadScene(currSceneName);
        }
        else if (state == goToNextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneName);
        }

    }
}
