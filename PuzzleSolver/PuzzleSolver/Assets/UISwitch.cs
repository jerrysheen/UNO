using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class UISwitch : UIControllBase
{
    public GameObject showObj;
    public string showObjName;
    public GameObject disableObj;
    public string disableObjName;
    // Start is called before the first frame update
    void Start()
    {
        showObj =
            showObjName == "" ? this.transform.Find("ShowObj").gameObject : this.transform.Find(showObjName).gameObject;
        
        disableObj =
            disableObjName == "" ? this.transform.Find("DisableObj").gameObject : this.transform.Find(disableObjName).gameObject;
        
        showObj.SetActive(false);
        disableObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        if (StoryManager.getInstance.currStory.currStoryLine == reactToLine || reactToLine == -1)
        {
            showObj.SetActive(true);
            disableObj.SetActive(false);
        }
    }
}
