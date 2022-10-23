using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICinnabarController : UIControllBase
{
    // Start is called before the first frame update
    //private Animator m_scrollAnimator;
    //public Sprite replaceSprite;
    // Start is called before the first frame update
    void Start()
    {
        //m_scrollAnimator = GetComponent<Animator>();
        //if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");

        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void onGameStateChange(int obj)
    {
        Debug.Log("State Change! ");
    }

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
    

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        //m_scrollAnimator.SetTrigger("Scroll");
        if ((StoryLine01) StoryManager.getInstance.currStory.currStoryLine == StoryLine01.Click_Scroll)
        {
            //  this.GetComponent<Image>().sprite = replaceSprite;
            StoryManager.getInstance.ValiDateState((int)StoryLine01.Click_Cinnabar);
        }
        
    }
}

