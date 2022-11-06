using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class AssignNewTransForm : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public Transform nextTrans;
    public void onGameStateChange(int obj)
    {
        if (obj == 3 || obj == 4 || StoryManager.getInstance.currStory.taskToDo[3] == 1 || StoryManager.getInstance.currStory.taskToDo[4] == 1 )
        {
            StartCoroutine(WaitCharecterChange(0.5f));

        }


    }

    // suspend execution for waitTime secondsf
    IEnumerator WaitCharecterChange(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (idleObj.activeSelf != charecterIdle.transform.Find("Riggings").gameObject.activeSelf)
        {
            if (idleObj.activeSelf)
            {
                nextTrans =  charecterMove.transform.Find("Show/bone_1/bone_2/bone_3/bone_4/bone_15");
                idleObj.transform.parent.gameObject.GetComponent<UIDragable>().followBy = nextTrans;
                idleObj.transform.parent.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);

                idleObj.transform.parent.gameObject.GetComponent<UIDragable>().followRotate = nextTrans.rotation.eulerAngles + new Vector3(0.0f, 0.0f, 180.0f);
            }
        }

    }

    
    
    public GameObject idleObj;
    public GameObject moveObj;

    public GameObject charecterIdle;
    public GameObject charecterMove;
    void Start()
    {
        idleObj = this.transform.Find("Idle").gameObject;
        moveObj = this.transform.Find("Move").gameObject;
        
        if(!idleObj || ! moveObj) Debug.LogError("Please assign obj");
        charecterIdle = GameObject.Find("CharacterIdle");
        charecterMove = GameObject.Find("CharecterMove");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
