using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene04CageAnim : UIControllBase
{
    // Start is called before the first frame update
    public Animator currAnim;
    public AnimationClip currClip;

    public float waitToNextLineTime = 3.0f;
    public bool shouldGotoNextScene = false;
    public int nextLineIndex = -1;
    public bool startToPlay = false;
    
    void Start()
    {
        currAnim = this.GetComponent<Animator>();
        
        //currAnim.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (StoryManager.getInstance.currStory.currStoryLine == reactToLine)
        {
            
            
            currAnim.SetBool("Dissapear", true);
        }
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
            //charecter.SetActive(true);
            StartCoroutine(PlayAnim());
        }
    }
    public override void OnClicked()
    {
        base.OnClicked();

    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(0.5f);
        currAnim.SetBool("CageFly",true);
       // yield return new WaitForSeconds(10.5f);
        //currAnim.SetBool("Dissapear",true);
        if (clip )
        {
            AudioManager.sceneAudioSource.Stop();
            AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
        }
        yield return new WaitForSeconds(1.5f);
        StoryManager.getInstance.ValiDateState(nextLineIndex);

    }
}
