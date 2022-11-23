using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
using StoryManagement;
using UnityEngine;

public class ClickDisplay : UIControllBase
{
    // Start is called before the first frame update
    //private Animator m_scrollAnimator;
    //public Sprite replaceSprite;
    public GameObject currentGameobj;
    public string GameObjName;
    public bool startState;
    public bool onClickShow = true;
    public bool triggerOnce = true;
    public bool needToGoToNextLine = true;
    public float showDelay;
    public int responseStoryLineIndex = -1;
    public int responseStoryLineIndex01 = -1;
    // public bool showWillTriggerNext = false;
    // public float disableDelay;
    // public int disableStoryLineIndex = -1;
    // public bool disableWillTriggerNext = false;
    //
    // Start is called before the first frame update
    void Start()
    {
        //m_scrollAnimator = GetComponent<Animator>();
        //if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");
        //responseStoryLineList = new List<int>();
        StoryManager.onGameStateChanged += onGameStateChange;
        if (GameObjName == "")
        {
            currentGameobj = this.transform.Find("ClickObj").gameObject;
        }
        else
        {
            currentGameobj = this.transform.Find(GameObjName).gameObject;

        }
        

        currentGameobj.SetActive(startState);
        if (this.GetComponent<CircleCollider2D>() != null && responseStoryLineIndex != 0 && responseStoryLineIndex != -1)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }
        
        if (this.GetComponent<PolygonCollider2D>() != null && responseStoryLineIndex != 0 && responseStoryLineIndex != -1)
        {
            this.GetComponent<PolygonCollider2D>().enabled = false;
        }
        

    }

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    
    private void onGameStateChange(int obj)
    {
        if (obj == responseStoryLineIndex || (responseStoryLineIndex == 1 || obj == 0) || (responseStoryLineIndex01 != -1 && responseStoryLineIndex01 == obj))
        {
            var Colliders = this.GetComponents<Collider2D>();
            foreach (var VARIABLE in Colliders)
            {
                VARIABLE.enabled = true;
            }
            

        }
        else
        {
            var Colliders = this.GetComponents<Collider2D>();
            foreach (var VARIABLE in Colliders)
            {
                VARIABLE.enabled = false;
            }
        }
    }

    
    // suspend execution for waitTime seconds
    IEnumerator Wait(float waitTime, GameObject go, bool Display, bool willTriggerNext, int currStoryLine)
    {
        yield return new WaitForSeconds(waitTime);
        go.SetActive(Display);
        if (willTriggerNext && StoryManager.getInstance.currStory.currStoryLine == currStoryLine)
        {
            StoryManager.getInstance.ValiDateState((currStoryLine + 1));
        }

    }
    
    
    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
    
    
    public override void OnClicked()
    {
        base.OnClicked();
        if (StoryManager.getInstance.currStory.currStoryLine == responseStoryLineIndex || reactToLine == -1 || (responseStoryLineIndex01 != -1 && responseStoryLineIndex01 == StoryManager.getInstance.currStory.currStoryLine))
        {
            //  this.GetComponent<Image>().sprite = replaceSprite;
            if (reactToLine != -1 && needToGoToNextLine)
            {
                StoryManager.getInstance.ValiDateState(responseStoryLineIndex + 1);
            }
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            currentGameobj.SetActive(onClickShow);
        }
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

