using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene05UIHide : MonoBehaviour
{
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = this.transform.Find("CharecterHide").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (StoryManager.getInstance.currStory.currStoryLine != 0)
        {
            obj.SetActive(false);
        }
    }
}
