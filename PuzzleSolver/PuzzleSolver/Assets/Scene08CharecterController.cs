using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene08CharecterController : UIControllBase
{
    public GameObject charecter;
    public int rotateIndex = -1;
    public int dispearIndex = -1;
    public int dispearIndex1 = -1;
    
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
        if (rotateIndex != -1 && state == rotateIndex)
        {
            var oldScale = charecter.transform.localScale;
            charecter.transform.localScale = new Vector3(-oldScale.x, oldScale.y, oldScale.z);
        }

        if (dispearIndex != -1 && state == dispearIndex)
        {
            this.gameObject.SetActive(false);
        }

        if (dispearIndex1 != -1 && state == dispearIndex1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
