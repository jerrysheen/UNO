using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class GoDisplay : UIControllBase
{
    // Start is called before the first frame update
    //private Animator m_scrollAnimator;
    //public Sprite replaceSprite;
    public GameObject currentGameobj;
    public bool startState;
    public float showDelay;
    public int showStoryLineIndex = -1;
    public bool showWillTriggerNext = false;
    public float disableDelay;
    public int disableStoryLineIndex = -1;
    public bool disableWillTriggerNext = false;
    
    // Start is called before the first frame update
    void Start()
    {
        //m_scrollAnimator = GetComponent<Animator>();
        //if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");

        StoryManager.onGameStateChanged += onGameStateChange;
        currentGameobj = this.transform.Find("GoDisplayObj").gameObject;
        currentGameobj.SetActive(startState);
    }

    private void onGameStateChange(int obj)
    {
        Debug.Log("State Change! ");
        if (obj == showStoryLineIndex)
        {
            StartCoroutine(Wait(showDelay, currentGameobj, true, showWillTriggerNext, showStoryLineIndex));
        }
        else if (obj == disableStoryLineIndex)
        {
            StartCoroutine(Wait(disableDelay, currentGameobj, false, disableWillTriggerNext, disableStoryLineIndex));

        }
    }

    
    // suspend execution for waitTime seconds
    IEnumerator Wait(float waitTime, GameObject go, bool Display, bool willTriggerNext, int currStoryLine)
    {
        yield return new WaitForSeconds(waitTime);
        go.SetActive(Display);
        if (willTriggerNext &&StoryManager.getInstance.currStory.currStoryLine == currStoryLine )
        {
            StoryManager.getInstance.ValiDateState((currStoryLine + 1));
        }

    }
    
    
    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
    


    // Update is called once per frame
    void Update()
    {
        
    }


}

