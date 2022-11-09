using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene08FatherController : UIControllBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public void onGameStateChange(int state)
    {
        if (state == reactToLine)
        {
            var localscale = this.transform.localScale;
            this.transform.localScale = new Vector3(-localscale.x, localscale.y, localscale.z);
        }

    }
}

