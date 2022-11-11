using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.EventSystems;

public class UIControllBase : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public int reactToLine = -1;
    public float delayTime = 0.0f;
    public AudioSource clip;
    public float volume;
    public AudioSource audio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnClicked()
    {
        Debug.Log(this.gameObject.name + "Clicked");
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Pressed!");
        //throw new System.NotImplementedException();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Released!");
        //throw new System.NotImplementedException();
    }


    public void playSoundOnce(float delayTime, AudioSource clip, float volume)
    {
        StartCoroutine(playOnce(delayTime, clip, volume));
    }

    IEnumerator playOnce(float delayTime, AudioSource clip, float volume)
    {
        yield return new WaitForSeconds(delayTime);
        clip.PlayOneShot(clip, volume);
    }

}
