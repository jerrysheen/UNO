using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene07Charecter : UIControllBase
{
    public int walkLeftStoryLine = -1;
    public float walkLefttime = 3.0f;
  
    public int walkRightStoryLine = -1;
    public float walkRighttime = 3.0f;
    public bool needDisapear = false;
    public GameObject currObj;
    public GameObject rightPath;
    public bool needGoToNextLine = false;
    public int nextLineIndex = -1;
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
        if (state == walkLeftStoryLine)
        {
            currObj.SetActive(true);
            renderer.flipX = true;
            //animator.enabled = true;
            StartCoroutine(RunLeft(walkLefttime));
        }
        else if(state == walkRightStoryLine)
        {
            currObj.SetActive(true);
            renderer.flipX = false;
            //animator.enabled = true;
            StartCoroutine(RunRight(walkRighttime));

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
        if (needGoToNextLine)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }
        this.gameObject.SetActive(!needDisapear);
    }

    IEnumerator RunRight(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            this.transform.position += Vector3.right* 290.0f * Time.deltaTime;
            yield return null;
        }

        if (needGoToNextLine)
        {
            StoryManager.getInstance.ValiDateState(nextLineIndex);
        }

        this.gameObject.SetActive(!needDisapear);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
