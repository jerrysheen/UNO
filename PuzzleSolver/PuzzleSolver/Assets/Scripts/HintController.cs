using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.UI;

public class HintController : MonoBehaviour
{
    // Start is called before the first frame update
    public int reactToProcessIndex;
    public GameObject original;
    public GameObject hint;

    public bool shouldStartCountDown;
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
            original.SetActive(false);
            hint.SetActive(true);
            // hint blink
            Color alphaChange = hint.GetComponent<Image>().color;
;
            if (alphaChange.a <= alphaMin)
            {
                isMinus = 1.0f;
            }
            else if (alphaChange.a >= alphaMax)
            {
                isMinus = -1.0f;
            }

            alphaChange.a += isMinus * Time.deltaTime * blinkSpeed;

            hint.GetComponent<Image>().color = alphaChange;

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

}
