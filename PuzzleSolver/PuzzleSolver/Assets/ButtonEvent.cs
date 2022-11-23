using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject Clicked;
    public GameObject Idled;
    // Start is called before the first frame update
    void Start()
    {
        Clicked.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Clicked.SetActive(true);
        Idled.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Clicked.SetActive(false);
        Idled.SetActive(true);
    }
}
