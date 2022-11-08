using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene08Door : UIControllBase
{
    public int reactToFalseResIndex = -1;
    public float waitRestartTime = 1.5f;
    public int reactToTrueResIndex = -1;
    public float waitNextSceneTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }


    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public void onGameStateChange(int state)
    {
        // if (state == reactToFalseResIndex)
        // {
        //     StartCoroutine(WaitToLoad("Scene08", waitRestartTime));
        // }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitToLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
