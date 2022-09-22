using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.UI;

public class ItemInteraction : MonoBehaviour
{
    public enum TriggerState
    {
        Interact,
        Leave
    }

    // Start is called before the first frame update
    public ItemBase.ItemType type;
    public float value;
    
    private bool m_isTriggered;
    private TriggerState m_state;
    
    void Start()
    {
        m_isTriggered = false;
        m_state = TriggerState.Leave;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == TriggerState.Interact)
        {
            OperateUI(true);
            DetermineInteract();
        }
    }

    private bool DetermineInteract()
    {
        if (m_state == TriggerState.Leave) return false;
        if (m_isTriggered) return false;
        if (Input.GetKey(KeyCode.E))
        {
            m_isTriggered = true;
            ItemManager.Instance.ChangeItemValue(type, value);
            HideItem();
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (!other.CompareTag("Player")) return;
        m_state = TriggerState.Interact;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        m_state = TriggerState.Leave;
        OperateUI(false);
    }

    private void HideItem()
    {
        OperateUI(false);
        this.gameObject.SetActive(false);
    }

    private void OperateUI(bool show)
    {
        
        var image = this.transform.Find("Image").gameObject;
        if (image != null)
        {
            if (show)
            {
                image.SetActive(true);
            }
            else
            {
                image.SetActive(false);
            }

        }
    }
}
