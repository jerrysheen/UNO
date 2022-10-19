using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIStampController : UIControllBase
{
    // Start is called before the first frame update
    //private Animator m_scrollAnimator;
    public Sprite replaceSprite;
    public Sprite original;
    private Image m_image;
    public float showTime = 2.0f;
    public float showTimeRed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //m_scrollAnimator = GetComponent<Animator>();
        //if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");

        StoryManager.onGameStateChanged += onGameStateChange;
        m_image = GetComponent<Image>();
        original = m_image.sprite;
        var tempColor = m_image.color;
        tempColor.a = 0.0f;
        m_image.color = tempColor;
    }

    private void onGameStateChange(int obj)
    {
        Debug.Log("State Change! ");
        StoryLine01 curr = (StoryLine01) obj;
        switch (curr)
        {
            case StoryLine01.Click_Scroll:
                StartCoroutine(Wait(showTime, original));
                break;
            case StoryLine01.Leave_FingerPrint:
                StartCoroutine(Wait(showTimeRed, replaceSprite));
                break;
        }
    }

    
    // suspend execution for waitTime seconds
    IEnumerator Wait(float waitTime, Sprite sprite)
    {
        yield return new WaitForSeconds(waitTime);
        var tempColor = m_image.color;
        tempColor.a = 1.0f;
        m_image.color = tempColor;
        m_image.sprite = sprite;
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
        if ((StoryLine01) StoryManager.getInstance.currStory.currStoryLine == StoryLine01.Click_Cinnabar)
        {
            StoryManager.getInstance.ValiDateState((int)StoryLine01.Leave_FingerPrint);
        }
        
    }
}

