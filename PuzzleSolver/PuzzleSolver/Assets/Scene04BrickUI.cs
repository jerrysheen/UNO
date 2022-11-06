using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene04BrickUI : UIControllBase
{
    // Start is called before the first frame update
    public Animation currAnim;
    public AnimationClip currClip;

    public float waitToNextLineTime = 3.0f;
    public bool shouldGotoNextScene = false;
    public int nextLineIndex = -1;
    public bool startToPlay = false;
    void Start()
    {
        currAnim = this.transform.Find("Brick").GetComponent<Animation>();
        
        //currAnim.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (startToPlay && StoryManager.getInstance.currStory.currStoryLine == reactToLine)
        {
            currAnim.clip = currClip;
            currAnim.Play(currClip.name);
        }
    }

    public override void OnClicked()
    {
        base.OnClicked();
        startToPlay = true;

        StartCoroutine(WaitToNextStoryLine(waitToNextLineTime));
    }

    IEnumerator WaitToNextStoryLine(float time)
    {
        yield return new WaitForSeconds(time);
        StoryManager.getInstance.ValiDateState(nextLineIndex);
    }
}
