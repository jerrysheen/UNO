using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class UIAnimatiorController : UIControllBase
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.enabled = false;
    }

    private void OnEnable()
    {
        StoryManager.onGameStateChanged += OnGameStateChange;
    }

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= OnGameStateChange;
    }

    public void OnGameStateChange(int obj)
    {
        if (obj == reactToLine)
        {
            animator.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
