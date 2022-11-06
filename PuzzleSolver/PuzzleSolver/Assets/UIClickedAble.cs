using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIClickedAble : UIControllBase
{
    // Start is called before the first frame update
    public List<int> reactToStoryLineIndex;
    public int gotoNext;
    public bool willDoSceneTransfer;
    public string nextScene;

    public float waitSceneTransferTime = 0.0f;
    void Start()
    {
        //reactToStoryLineIndex = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        foreach (var VARIABLE in reactToStoryLineIndex)
        {
            if (StoryManager.getInstance.currStory.currStoryLine == VARIABLE)
            {
                StoryManager.getInstance.ValiDateState(gotoNext);
                foreach(Collider2D c in GetComponents<Collider2D> ())
                {
                    c.enabled = false;
                }
                
            }
        }
        if (willDoSceneTransfer)
        {
            StartCoroutine(WaitToNextScene(waitSceneTransferTime));
        }

        
    }

    IEnumerator WaitToNextScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nextScene);
    }
}
