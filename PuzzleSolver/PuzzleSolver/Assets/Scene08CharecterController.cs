using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene08CharecterController : UIControllBase
{
    public GameObject charecter;
    void Start()
    {
        charecter = this.transform.Find("Show").gameObject;
    }

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
        if (state == reactToLine)
        {
            var oldScale = charecter.transform.localScale;
            charecter.transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
        }
    }
}
