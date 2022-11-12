using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene08ButterFly : UIControllBase
{
    // Start is called before the first frame update
    public GameObject butterFly;
    public float flyTime;
    public bool startCountDown;
    public bool firstTimeTrigger;

    private float count;
    void Start()
    {
        butterFly = this.transform.Find("ButterFly").gameObject;
        butterFly.SetActive(false);
        startCountDown = false;
        firstTimeTrigger = true;
        GetComponent<Animator>().enabled = false;
        count = flyTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountDown)
        {
            count -= Time.deltaTime;
            if (count <= 0 && firstTimeTrigger)
            {
                firstTimeTrigger = false;
                StoryManager.getInstance.ValiDateState(reactToLine + 1);
            }
        }
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
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            butterFly.SetActive(true);
            GetComponent<Animator>().enabled = true;
            startCountDown = true;
            
        }

    }
}
