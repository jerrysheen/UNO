using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    // Start is called before the first frame update
    public int reactToProcessIndex;
    public Material dialogueShowMat;
    public GameObject dialogue;
    public GameObject text;
    private bool shouldStartCountDown;
    public float DelayToShowTime = 3.0f;
    public float DelayDisableTime = 3.0f;
    private float countdown;
    public float gotoNextStoryLineThreash;
    public bool needSendGotoNextInfo;
    public float blinkSpeed = .3f;
    public float blinkMaxTime = 5.0f;
    private float isMinus = 1.0f;

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    void Start()
    {

        
        shouldStartCountDown = false;
        countdown = 0.0f;
        dialogue = this.transform.Find("Dialogue").gameObject;
        text = dialogue.transform.Find("Text").gameObject;
        if (!dialogue)
        {
            Debug.LogError("please assign obj first");
        }
        dialogue.SetActive(false);
        dialogueShowMat = text.GetComponent<Image>().material;
    }

    private void Update()
    {
        
    }

    private void onGameStateChange(int obj)
    {
        if (reactToProcessIndex == obj )
        {
            StartCoroutine(DelayTime(DelayToShowTime, DelayDisableTime));
            
        }
        else
        {
            dialogue.SetActive(false);
        }

        Debug.Log("State Change! ");
    }
    
    IEnumerator DelayTime (float beforeShowtime, float disableDelay)
    {
        yield return new WaitForSeconds(beforeShowtime);
        shouldStartCountDown = true;
        
        // show dialogue:
        dialogue.SetActive(true);
        while (countdown < blinkMaxTime){
            countdown += Time.deltaTime * blinkSpeed;
            //Debug.Log(countdown);
            dialogueShowMat.SetFloat("_ReadSpeed", countdown);
            yield return null;
        }

        float currCountDown = countdown;
        // disable
        while (countdown  < currCountDown + disableDelay)
        {
//            Debug.Log(countdown);
            countdown += Time.deltaTime;
            yield return null;
        }
        dialogue.SetActive(false);
        if (needSendGotoNextInfo && StoryManager.getInstance.currStory.currStoryLine == reactToProcessIndex)
        {
            StoryManager.getInstance.ValiDateState((int)reactToProcessIndex + 1);
        }
    }
    
    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
}
