using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class UIDisplay : UIControllBase
{

    public GameObject showObj;
    public string showObjName;
    public bool needShow = false;
    public float delayDisplayTime = 0.0f;
    public bool reactByTaskList = false;
    public int reactStoryLineIndex = -1;
    // Start is called before the first frame update

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
        if (reactByTaskList)
        {
            if (StoryManager.getInstance.currStory.taskToDo[reactStoryLineIndex] == 1)
            {
                StartCoroutine(Wait(delayDisplayTime));
            }
        }
        else
        {
            if (reactStoryLineIndex == state)
            {
                StartCoroutine(Wait(delayDisplayTime));
            }
        }
        
    }

    void Start()
    {
        if (showObjName == "")
        {
            showObj = this.transform.Find("Show").gameObject;
        }
        else
        {
            showObj = this.transform.Find(showObjName).gameObject;
        }

        showObj.SetActive(!needShow);
 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnClicked()
    {
        base.OnClicked();

    }

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        showObj.SetActive(needShow);

    }
}
