using System.Collections;
using System.Collections.Generic;
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
    public float showDelay;
    public int responseStoryLineIndex = -1;
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

    private void onGameStateChange(int obj)
    {
        // 防止影响其他点击效果
        if (obj == responseStoryLineIndex || (responseStoryLineIndex == 1 || obj == 0))
        {
            var Colliders = this.GetComponents<Collider>();
            foreach (var VARIABLE in Colliders)
            {
                VARIABLE.enabled = true;
            }
        }
        else
        {
            var Colliders = this.GetComponents<Collider>();
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
        if (StoryManager.getInstance.currStory.currStoryLine == responseStoryLineIndex || reactToLine == -1)
        {
            //  this.GetComponent<Image>().sprite = replaceSprite;
            if (reactToLine != -1)
            {
                StoryManager.getInstance.ValiDateState(responseStoryLineIndex + 1);
            }
            currentGameobj.SetActive(onClickShow);
        }
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

