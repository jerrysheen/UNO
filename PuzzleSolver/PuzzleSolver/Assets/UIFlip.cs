using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class UIFlip : UIControllBase
{
    public bool flipX;
    public bool flipY;
    public bool needGoToNext;
    public int nextLineIndex;
    public float flipDelayTime;
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

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;

    }
    
    public void onGameStateChange(int state)
    {
        if (state == reactToLine)
        {
            StartCoroutine(Flip(flipDelayTime));

        }
    }

    IEnumerator Flip(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        var localScale = this.transform.localScale;
        if (flipX)
        {
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }

        if (flipY)
        {
            transform.localScale = new Vector3(localScale.x, -localScale.y, localScale.z);
        }
        if (needGoToNext)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
    }
}
