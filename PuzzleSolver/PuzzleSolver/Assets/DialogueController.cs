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
    public float countDownTime = 5.0f;
    public float DelayToShowTime = 3.0f;
    private float countdown;

    public float blinkSpeed = 1.0f;
    private float isMinus = 1.0f;
    void Start()
    {

        StoryManager.onGameStateChanged += onGameStateChange;
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
        if (shouldStartCountDown)
        {
            dialogue.SetActive(true);
            countdown += Time.deltaTime * blinkSpeed;
            dialogueShowMat.SetFloat("_ReadSpeed", countdown);
        }


    }

    private void onGameStateChange(int obj)
    {
        if (reactToProcessIndex == obj)
        {
            StartCoroutine(DelayTime(DelayToShowTime));
            
        }
        else
        {
        }

        Debug.Log("State Change! ");
    }
    
    IEnumerator DelayTime (float time)
    {
        yield return new WaitForSeconds(time);
        shouldStartCountDown = true;
    }
    
    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
}
