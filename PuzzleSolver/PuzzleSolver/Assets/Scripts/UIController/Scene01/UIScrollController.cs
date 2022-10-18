using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScrollController : UIControllBase
{
    // Start is called before the first frame update
    private Animator m_scrollAnimator;
    // Start is called before the first frame update
    void Start()
    {
        m_scrollAnimator = GetComponent<Animator>();
        if(m_scrollAnimator == null) Debug.LogError("Can't find Animator ！");
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
        
    }

    public override void OnClicked()
    {
        base.OnClicked();
        m_scrollAnimator.SetTrigger("Scroll");
    }
}
