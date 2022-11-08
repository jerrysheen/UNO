using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene08CharecterMove : MonoBehaviour
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
    void Start()
    {
        charecter = this.transform.Find("CharecterMove").gameObject;
        charecter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShow)
        {
            if(!charecter.activeSelf) charecter.SetActive(true);
            this.transform.position += Vector3.left * Time.deltaTime * speed;
        }
        else
        {
            if(charecter.activeSelf) charecter.SetActive(false);
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
        if (state == reactToFalseResIndex)
        {
            //charecter.SetActive(true);
            shouldShow = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == colliderName)
        {
            shouldShow = false;
            if (StoryManager.getInstance.currStory.currStoryLine == reactToFalseResIndex)
            {
                StartCoroutine(WaitToLoad("Scene08", waitRestartTime));
            }
        }
    }
    
    IEnumerator WaitToLoad(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
