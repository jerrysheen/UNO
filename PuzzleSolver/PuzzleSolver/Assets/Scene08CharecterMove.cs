using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene08CharecterMove : UIControllBase
{
    // Start is called before the first frame update

    public string colliderName;
    public int reactToFalseResIndex = -1;
    public float waitRestartTime = 1.5f;
    public int reactToTrueResIndex = -1;
    public float waitNextSceneTime = 0.0f;

    public GameObject charecter;
    public bool shouldShow = false;
    public float speed = 250.0f;
    public float moveTime = 2.5f;
    void Start()
    {
        charecter = this.transform.Find("CharecterMove").gameObject;
        charecter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (shouldShow)
        // {
        //     if(!charecter.activeSelf) charecter.SetActive(true);
        //     this.transform.position += Vector3.left * Time.deltaTime * speed;
        // }
        // else
        // {
        //     if(charecter.activeSelf) charecter.SetActive(false);
        // }

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
        if (state == reactToFalseResIndex || state == reactToTrueResIndex)
        {
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();
                AudioManager.getInstance.PlaySceneAudio(clip, volume, loop, delay);
            }
            //charecter.SetActive(true);
            shouldShow = true;
            StartCoroutine(MoveToDoor(moveTime));
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Door")
        {
            this.gameObject.SetActive(false);
            if (clip )
            {
                AudioManager.sceneAudioSource.Stop();

            }
        }
    }
    
    IEnumerator WaitToLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
    
    
    IEnumerator MoveToDoor(float countDown)
    {
        yield return new WaitForSeconds(0.5f);
        if(!charecter.activeSelf) charecter.SetActive(true);

        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            this.transform.position += Vector3.left * Time.deltaTime * speed;
            yield return null;
        }

        //     this.transform.position += Vector3.left * Time.deltaTime * speed;
    }
}
