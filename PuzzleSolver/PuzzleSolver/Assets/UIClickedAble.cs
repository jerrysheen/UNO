using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIClickedAble : UIControllBase
{
    // Start is called before the first frame update
    public List<int> reactToStoryLineIndex;
    public int gotoNext;
    public bool willDoSceneTransfer;
    public string nextScene;

    public float waitSceneTransferTime = 0.0f;

    public bool willDisableAfterClick = false;
    void Start()
    {
        foreach (var VARIABLE in reactToStoryLineIndex)
        {
            if (StoryManager.getInstance != null && StoryManager.getInstance.currStory!= null && StoryManager.getInstance.currStory.currStoryLine == VARIABLE)
            {
                foreach(Collider2D c in GetComponents<Collider2D>())
                {
                    c.enabled = true;
                }
            }
            else
            {
                //reactToStoryLineIndex = new List<int>();
                foreach(Collider2D c in GetComponents<Collider2D>())
                {
                    c.enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
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

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }


    public void onGameStateChange(int state)
    {
        if (reactToStoryLineIndex == null) return;
        foreach (var VARIABLE in reactToStoryLineIndex)
        {
            var colliders = this.GetComponents<Collider2D>();
            if (colliders == null || colliders.Length <= 0) return;
            if (state == VARIABLE)
            {
                foreach(Collider2D c in colliders)
                {
                    c.enabled = true;
                }
            }
        }
    }

    public override void OnClicked()
    {
        base.OnClicked();
        foreach (var VARIABLE in reactToStoryLineIndex)
        {
            if (StoryManager.getInstance.currStory.currStoryLine == VARIABLE)
            {
                StoryManager.getInstance.ValiDateState(gotoNext);
                if (clip )
                {
                    AudioManager.sceneAudioSource.Stop();
                    AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
                }
                foreach(Collider2D c in GetComponents<Collider2D> ())
                {
                    c.enabled = false;
                }
            }
        }

        if (willDisableAfterClick)
        {
            this.gameObject.SetActive(false);
        }

        if (willDoSceneTransfer)
        {
            StartCoroutine(WaitToNextScene(waitSceneTransferTime));
        }

        
    }

    IEnumerator WaitToNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        if (clip )
        {
            AudioManager.sceneAudioSource.Stop();
        }
        SceneManager.LoadScene(nextScene);
    }
}
