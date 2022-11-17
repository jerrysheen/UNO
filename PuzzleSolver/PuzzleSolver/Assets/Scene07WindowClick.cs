using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene07WindowClick : UIControllBase
{
    public GameObject ui;
    public GameObject dialogue;
    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
        //dialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        ui.SetActive(true);
        dialogue.SetActive(true);
        dialogue.GetComponent<DialogueController>().DisplayDialogue();
        foreach(Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }
    }
}
