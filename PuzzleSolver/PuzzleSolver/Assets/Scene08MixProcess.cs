using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene08MixProcess : MonoBehaviour
{
    // Start is called before the first frame update
    public string other0Name;
    public string show0Name;
    public GameObject show0Obj;

    
    public string other1Name;
    public string show1Name;
    public GameObject show1Obj;

    
    public string show2Name;
    public GameObject show2Obj;

    public bool firstTimeTrigger = true;
    public float showMixTime = 1.5f;
    void Start()
    {
        if (show0Name != "")
        {
            show0Obj = this.transform.Find(show0Name).gameObject;
        }
        
        if (show1Name != "")
        {
            show1Obj = this.transform.Find(show1Name).gameObject;
        }
        
        if (show0Name != "")
        {
            show2Obj = this.transform.Find(show2Name).gameObject;
        }

        firstTimeTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (show0Obj && show1Obj && firstTimeTrigger)
        {
            StartCoroutine(WaitToShow(showMixTime));
            firstTimeTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "") return;
        if (other.name == other0Name)
        {
            show0Obj.SetActive(true);
        }
        else if (other.name == other1Name)
        {
            show1Obj.SetActive(true);
        }
    }

    IEnumerator WaitToShow(float showMixTime)
    {
        if(show0Obj) show0Obj.SetActive(false);
        if(show1Obj) show0Obj.SetActive(false);
        yield return new WaitForSeconds(showMixTime);
        if(show2Obj) show2Obj.SetActive(true);
    }
}
