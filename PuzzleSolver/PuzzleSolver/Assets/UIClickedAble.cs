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
    void Start()
    {
        reactToStoryLineIndex = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        if (willDoSceneTransfer)
        {
            SceneManager.LoadScene(nextScene);
        }
        foreach (var VARIABLE in reactToStoryLineIndex)
        {
            if (StoryManager.getInstance.currStory.currStoryLine == VARIABLE)
            {
                StoryManager.getInstance.ValiDateState(gotoNext);
                foreach(Collider c in GetComponents<Collider> ())
                {
                    c.enabled = false;
                }
                
            }
        }
        
        
    }
}
