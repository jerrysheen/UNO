using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class TearControl : UIControllBase
{
    // Start is called before the first frame update
    //private Animator m_scrollAnimator;
    //public Sprite replaceSprite;
    public GameObject Tear0;
    //public GameObject Tear1;
    public Animator animatior;
    private Vector3 tempPos;
    public bool isStart;
    // Start is called before the first frame update
    void Start()
    {
        //m_scrollAnimator = GetComponent<Animator>();
        //if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");
        isStart = true;
        StoryManager.onGameStateChanged += onGameStateChange;
        Tear0 = this.transform.Find("Tear0").gameObject;
        animatior = GetComponent<Animator>();
        //Tear1 = this.transform.Find("Tear1").gameObject;
        //Tear2 = this.transform.Find("Tear2").gameObject;
        if (!Tear0 || animatior)
        {
            Debug.LogError("Please Init Tear Gameobj");
        }

        tempPos = gameObject.transform.position;
    }

    private void onGameStateChange(int obj)
    {
        Debug.Log("State Change! to" +  obj);
        if (obj == (int) StoryLine02.Start)
        {
            isStart = true;
        }
        else
        {
            isStart = false;
        }
    }



    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }
    

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            StartTearMoveMent();
            
        }

    }

    private void StartTearMoveMent()
    {
        if (animatior.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 < 0.001f)
        {
            tempPos = Tear0.transform.position;
        }
        var TempRotation = Tear0.transform.rotation.eulerAngles;
        //Tear0.transform.position = new Vector3(TempPos.x, TempPos.y, TempPos)
        Tear0.transform.rotation = Quaternion.Euler(new Vector3(TempRotation.x, TempRotation.y, 0.0f));
        Tear0.transform.position = new Vector3(tempPos.x, Tear0.transform.position.y, 0.0f);

    }
    
    public override void OnClicked()
    {
        base.OnClicked();
        //m_scrollAnimator.SetTrigger("Scroll");
        if ((StoryLine01) StoryManager.getInstance.currStory.currStoryLine == StoryLine01.Click_Cinnabar)
        {
            StoryManager.getInstance.ValiDateState((int)StoryLine01.Leave_FingerPrint);
        }
        
    }
}