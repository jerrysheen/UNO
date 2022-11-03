using System;
using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIDragable : UIControllBase
{
    private bool firstTimeEnter;
    public bool triggerDrag;
    private Vector3 LastPos;

    public GameObject idleObj;

    public GameObject moveObj;
    public int reactToStoryLine = -1;
    public int parallexStoryLineBefore = 0;
    public int parallexStoryLineAfter = 0;
    public bool disableWhenReachToEnd;
    public bool needResetParent;
    public float disableTime = 0.0f;
    public string endPositionColliderName = "";
    // Start is called before the first frame update
    public bool needFollow;
    public Transform followBy;
    public Vector3 followRotate;
    public bool notYetDrag;
    void Start()
    {
        notYetDrag = true;
        firstTimeEnter = true;
        LastPos = this.transform.position;
        if (needFollow)
        {
            followRotate = followBy.rotation.eulerAngles;
        }

        triggerDrag = false;

        if(idleObj)idleObj = this.transform.Find("Idle").gameObject;
        if(moveObj)moveObj = this.transform.Find("Move").gameObject;
        
       // if(!idleObj || ! moveObj) Debug.LogError("Please assign obj");
        if(idleObj)idleObj.SetActive(true);
        if(moveObj)moveObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (notYetDrag)
        {
            if (needFollow && idleObj && moveObj)
            {
                var diff = followBy.rotation.eulerAngles - followRotate;
                var currPos = idleObj.transform.rotation.eulerAngles;
                if (followBy.rotation.eulerAngles.y >= 179.90f)
                {
                    idleObj.transform.rotation = Quaternion.Euler(currPos - diff);
                    moveObj.transform.rotation = Quaternion.Euler(currPos - diff);
                }
                else
                {
                    idleObj.transform.rotation = Quaternion.Euler(currPos + diff);
                    moveObj.transform.rotation = Quaternion.Euler(currPos + diff);
                }

                idleObj.transform.position = followBy.transform.position;
                moveObj.transform.position = followBy.transform.position;
                followRotate = followBy.rotation.eulerAngles;
            }
        }

        if (triggerDrag)
        {
            notYetDrag = false;
            if (Input.GetMouseButton(0) || firstTimeEnter)
            {
//                Debug.LogError(firstTimeEnter);
                if (firstTimeEnter)
                {
                    LastPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
                    firstTimeEnter = false;
                }
                else
                {
                    
                    var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
                    var diff = mousePos - LastPos;
  //                  Debug.Log(diff);
                    var currPos = this.transform.position;
                    this.transform.position = currPos + diff;
                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
                    LastPos = mousePos;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Move");
                firstTimeEnter = true;
                triggerDrag = false;
            }
        }


        
    }

    // public override void OnPointerDown(PointerEventData eventData)
    // {
    //     base.OnPointerDown(eventData);
    //     Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
    //     this.transform.position = worldPosition;
    // }

    public override void OnClicked()
    {
        base.OnClicked();
        for (int i = reactToStoryLine - parallexStoryLineBefore; i <= reactToStoryLine + parallexStoryLineAfter; i++) 
        {
            if (StoryManager.getInstance.currStory.currStoryLine == i)
            {
                triggerDrag = true;
                if(idleObj)idleObj.SetActive(false);
                if(moveObj)moveObj.SetActive(true);
                // // z 表示距离相机的距离
                // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.gameObject.transform.position.z));
                // this.transform.position = worldPosition;
                // if (needResetParent)
                // {
                //     this.transform.parent = new RectTransform();
                //     this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
                // }
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == endPositionColliderName)
        {
            StartCoroutine(Wait(disableTime));
        }

        Debug.Log(other.name);
        var go = other.gameObject;
        
    }
    
    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (StoryManager.getInstance)
        {
            // 不能以这个为数， 最后直接跳到总共完成的那边去应该。
            // 应该自己在这边注册， 而不是去manager里面注册。
            //StoryManager.getInstance.ValiDateState(reactToStoryLine);
            StoryManager.getInstance.ValiDateCurrent(reactToStoryLine);

            bool allDone = true;
            for (int i = reactToStoryLine - parallexStoryLineBefore; i <= reactToStoryLine + parallexStoryLineAfter; i++) 
            {
                if (StoryManager.getInstance.currStory.taskToDo[i] == 0)
                {
                    allDone = false;
                }
            }

            if (allDone)
            {
                StoryManager.getInstance.ValiDateState(reactToStoryLine + parallexStoryLineAfter + 1);
            }
            

        }

       
        
        if (disableWhenReachToEnd)
        {
            moveObj.SetActive(false);
            var collider = this.GetComponent<CapsuleCollider2D>();
            if (collider)
            {
                collider.enabled = false;
            }
            var collider00 = this.GetComponent<CircleCollider2D>();
            if (collider00)
            {
                collider00.enabled = false;
            }
            var collider01 = this.GetComponent<PolygonCollider2D>();
            if (collider01)
            {
                collider01.enabled = false;
            }

        }
    }
}
