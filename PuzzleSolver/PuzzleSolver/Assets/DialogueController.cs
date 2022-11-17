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
    public Material dialogueShowMat01;
    public GameObject dialogue;
    public GameObject text;
    public GameObject text01;
    private bool shouldStartCountDown;
    public float DelayToShowTime = 3.0f;
    public float DelayDisableTime = 3.0f;
    private float countdown;
    public float gotoNextStoryLineThreash;
    public bool needSendGotoNextInfo;
    public int nextLineIndex;
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
        text01 = dialogue.transform.Find("Text01")?.gameObject;
        //text01 = dialogue.transform.Find("");
        if (!dialogue)
        {
            Debug.LogError("please assign obj first");
        }
        //dialogue.SetActive(false);
        dialogueShowMat = text.GetComponent<Image>().material;
        dialogueShowMat01 = text01?.GetComponent<Image>().material;
        if (dialogueShowMat01 != null)
        {
            dialogueShowMat01.SetFloat("_ReadSpeed", 0.0f);
        }
        dialogue.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void onGameStateChange(int obj)
    {
        if (reactToProcessIndex == obj )
        {
            DisplayDialogue();
        }
        else
        {
           
        }

        Debug.Log("State Change! ");
    }

    public void DisplayDialogue()
    {
        StartCoroutine(DelayTime(DelayToShowTime, DelayDisableTime));
    }

    IEnumerator DelayTime (float beforeShowtime, float disableDelay)
    {


        yield return new WaitForSeconds(beforeShowtime);
        shouldStartCountDown = true;
        
        // show dialogue:
        dialogue.SetActive(true);
        countdown = 0.0f;
        while (countdown < blinkMaxTime){
            countdown += Time.deltaTime * blinkSpeed;
            //Debug.Log(countdown);
            dialogueShowMat.SetFloat("_ReadSpeed", countdown);
//            Debug.Log(countdown);
            yield return null;
        }

        if (text01 != null)
        {
            countdown = 0.0f;
            while (countdown < blinkMaxTime){
                countdown += Time.deltaTime * blinkSpeed;
                //Debug.Log(countdown);
                dialogueShowMat01.SetFloat("_ReadSpeed", countdown);
                Debug.Log(countdown);
                yield return null;
            }
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
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
    }
    
    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
}
