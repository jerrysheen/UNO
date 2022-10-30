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
        currentGameobj = this.transform.Find("ClickObj").gameObject;
        currentGameobj.SetActive(startState);
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void onGameStateChange(int obj)
    {
        // 防止影响其他点击效果
        if (obj == responseStoryLineIndex || (responseStoryLineIndex == 1 && obj == 0))
        {
            this.GetComponent<CircleCollider2D>().enabled = true;
        }
        else
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
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
        if (StoryManager.getInstance.currStory.currStoryLine == responseStoryLineIndex)
        {
            //  this.GetComponent<Image>().sprite = replaceSprite;
            StoryManager.getInstance.ValiDateState(responseStoryLineIndex + 1);
            currentGameobj.SetActive(onClickShow);
        }
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

