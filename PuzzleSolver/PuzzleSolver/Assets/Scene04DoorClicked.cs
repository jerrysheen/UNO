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
        if (StoryManager.getInstance.currStory.currStoryLine <= 1)
        {
            StartCoroutine(BackToOrigin(waitTime));
        }
        else if (StoryManager.getInstance.currStory.currStoryLine >= (int) StoryLine04.Charecter_Move)
        {
            // do people move
            
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
