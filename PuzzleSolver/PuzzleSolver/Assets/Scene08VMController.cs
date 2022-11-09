using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StoryManagement;
using UnityEngine;

public class Scene08VMController : MonoBehaviour
{
// Start is called before the first frame update
    //private Animator m_scrollAnimator;
    //public Sprite replaceSprite;
    [SerializeField] private GameObject vcamNear;
    [SerializeField] private GameObject vcamFar;
    [SerializeField] private GameObject vcamClose;

    public float waitTimeFar;
    public float waitTimeClose;
    public int switchTofarStoryLine = 0;
    public int switchToNearStoryLine = 0;

    public bool needToGoToNext = false;

    public int nextLineIndex = 8;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    void Start()
    {
        vcamNear = this.transform.Find("CM Vcam Near").gameObject;
        vcamFar = this.transform.Find("CM Vcam Far").gameObject;
        vcamClose = this.transform.Find("CM Vcam Close").gameObject;
        vcamNear.SetActive(true);
        vcamFar.SetActive(false);
        vcamClose.SetActive(false);
        if (!vcamFar || !vcamNear || !vcamClose)
        {
            Debug.LogError("please assign vcam");
        }
    }

    private void onGameStateChange(int obj)
    {
        Debug.Log("State Change! to" +  obj);
        StoryLine02 curr = (StoryLine02) obj;
//        Debug.LogError(curr);
        if (obj == switchTofarStoryLine)
        {
            StartCoroutine(Wait(waitTimeFar, true));
        }
        else if (obj == switchToNearStoryLine)
        {
           // StartCoroutine(Wait(waitTime, false));
           StartCoroutine(Wait00(waitTimeClose));

        }

    }

    
    // suspend execution for waitTime seconds
    IEnumerator Wait(float waitTime, bool display)
    {
        yield return new WaitForSeconds(waitTime);
        vcamFar.SetActive(display);
        if(needToGoToNext)StoryManager.getInstance.ValiDateState(nextLineIndex);
    }
    
    IEnumerator Wait00(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        vcamClose.SetActive(true);
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
