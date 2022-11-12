using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene05Charecter : UIControllBase
{
    public int walkLeftStoryLine = -1;
    public int walkRightStoryLine = -1;

    public GameObject currObj;
    public GameObject rightPath;
    
    public SpriteRenderer renderer;
    public Animator animator;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        currObj = this.transform.Find("CharecterMove").gameObject;
        //rightPath = GameObject.Find("RightPath");
        renderer = currObj.GetComponent<SpriteRenderer>();
        //animator = currObj.GetComponent<Animator>();
        //animator.enabled = false;
        currObj.SetActive(false);
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
        if (state == 1)
        {
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            currObj.SetActive(true);
            renderer.flipX = true;
            //animator.enabled = true;
            StartCoroutine(RunLeft(3.5f));
        }
        else if(state == 2)
        {
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            currObj.SetActive(true);
            renderer.flipX = false;
            //animator.enabled = true;
            StartCoroutine(RunRight(5.5f));

        }
    }

    IEnumerator RunLeft(float time) 
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            this.transform.position += Vector3.left * 290.0f * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RunRight(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            this.transform.position += Vector3.right* 290.0f * Time.deltaTime;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (clip )
        {
            AudioManager.sceneAudioSource.Stop();
        }
    }
}
