using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIalphaChange : UIControllBase
{
    // Start is called before the first frame update
    public float disapearTime = 3.0f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnEnable()
    {
        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDisable()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;
    }

    public void onGameStateChange(int state)
    {
        if (state == reactToLine)
        {
            StartCoroutine(ColorDecrease(disapearTime));
        }

    }

    IEnumerator ColorDecrease(float time)
    {
        var img = this.GetComponent<Image>();
        while (time >= 0.0f)
        {
            
            Color color = img.color;
            color.a -= 1.0f / disapearTime * Time.deltaTime;
            time -= Time.deltaTime;
            color.a = color.a < 0.0f ? 0.0f : color.a;
            img.color = color;
            //Debug.Log(".....");
            yield return null;
        }
    }
}
