using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene04DoorClicked : UIControllBase
{
    // Start is called before the first frame update
    public GameObject dialogueObj;
    public string dialogueObjName;
    public string backtoSceneName;
    public float waitTime = 5.0f;
    public int trueStoryLine = 15;
    void Start()
    {
        dialogueObj = GameObject.Find(dialogueObjName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        if (StoryManager.getInstance.currStory.currStoryLine == 0)
        {
            StartCoroutine(BackToOrigin(waitTime));
        }
        else if(StoryManager.getInstance.currStory.currStoryLine == trueStoryLine)
        {
            // do people move
            StoryManager.getInstance.currStory.currStoryLine += 1;
        }
    }
    
    IEnumerator BackToOrigin(float waitTime)
    {   
        DialogueController controller = dialogueObj.GetComponent<DialogueController>();
        controller.DisplayDialogue();
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(backtoSceneName);
    } 
}
