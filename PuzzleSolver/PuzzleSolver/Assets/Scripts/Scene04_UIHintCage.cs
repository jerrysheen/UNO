using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.UI;

public class Scene04_UIHintCage : UIControllBase
{
    // Start is called before the first frame update
    public int reactToProcessIndex;
    public GameObject original;
    public GameObject hint;
    public bool shouldDisableOriginal;

    public bool shouldStartCountDown;
    public bool shouldGoToNextStoryLine = false;
    public int nextStoryLineIndex;
    public float countDownTime = 5.0f;
    public float delayTime = 5.0f;
    private float countdown;

    public float alphaMin = 0.7f;
    public float alphaMax = 1.0f;
    public float blinkSpeed = 1.0f;
    private float isMinus = 1.0f;
    void Start()
    {

        StoryManager.onGameStateChanged += onGameStateChange;
        countdown = countDownTime;
        original = this.transform.Find("NoHint").gameObject;
        hint = this.transform.Find("Hint").gameObject;
        if (!original || !hint)
        {
            Debug.LogError("please assign obj first");
        }
        hint.SetActive(false);
    }

    private void Update()
    {
        if (StoryManager.getInstance != null && StoryManager.getInstance.currStory.currStoryLine != reactToProcessIndex)
        {
            return;
        }
        if (shouldStartCountDown)
        {
            countdown -= Time.deltaTime;
        }

        if (countdown <= 0 && shouldStartCountDown)
        { 
            if(shouldDisableOriginal)original.SetActive(false);
            hint.SetActive(true);
            // hint blink
            var Img = hint.GetComponent<Image>();
            Color alphaChange;
            if (Img)
            {
                alphaChange = hint.GetComponent<Image>().color;
            }
            else
            {
                alphaChange = hint.transform.Find("Front").GetComponent<SpriteRenderer>().color;
            }
            
            if (alphaChange.a <= alphaMin)
            {
                isMinus = 1.0f;
            }
            else if (alphaChange.a >= alphaMax)
            {
                isMinus = -1.0f;
            }

            alphaChange.a += isMinus * Time.deltaTime * blinkSpeed;

            hint.transform.Find("Front").GetComponent<SpriteRenderer>().color = alphaChange;

        }
    }

    private void onGameStateChange(int obj)
    {
        if (reactToProcessIndex == obj)
        {
            StartCoroutine(DelayTime(delayTime));
            
        }
        else
        {
            shouldStartCountDown = false;
        }

        Debug.Log("State Change! ");
    }

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
    
   
    IEnumerator DelayTime (float time)
    {
        yield return new WaitForSeconds(time);
        shouldStartCountDown = true;
    }

    public override void OnClicked()
    {
        base.OnClicked();
        if (StoryManager.getInstance.currStory.currStoryLine == reactToProcessIndex)
        {
            //  this.GetComponent<Image>().sprite = replaceSprite;
            if (shouldGoToNextStoryLine)
            {
                StoryManager.getInstance.ValiDateState(nextStoryLineIndex);
            }

            if(shouldDisableOriginal)original.SetActive(true);
            hint.SetActive(false);
        }
        
    }
}
