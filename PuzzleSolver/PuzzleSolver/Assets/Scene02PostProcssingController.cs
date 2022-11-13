using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Scene02PostProcssingController : UIControllBase
{
    
    private Volume volumn;
    private Bloom bloom;
    private Vignette vignette;

    public bool shouldChangeVignette;
    public float vigChangeTime;
    public float vigStartValue;
    public float vigEndValue;
    public Vector2 vigCenter;
    public bool shouldChangeBloom;
    public float bloomChangeTime;
    public float bloomStartValue;
    public float bloomvigEndValue;
    // Start is called before the first frame update
    void Start()
    {
        volumn = GetComponent<Volume>();
        volumn.profile.TryGet(out bloom);
        volumn.profile.TryGet(out vignette);
        if (vignette)
        {
            vignette.intensity.value = vigStartValue;
            vignette.center = new Vector2Parameter(vigCenter);
        }
    }
    
    private void OnEnable()
    {

        StoryManager.onGameStateChanged += onGameStateChange;
    }

    private void OnDestroy()
    {
        StoryManager.onGameStateChanged -= onGameStateChange;

    }

    public void onGameStateChange(int state)
    {
        if (reactToLine == state)
        {
            if (shouldChangeVignette && vignette)
            {
                
                StartCoroutine(ChangeVignette(vigChangeTime, vigStartValue, vigEndValue));
            }

            if (shouldChangeBloom && bloom)
            {
                StartCoroutine(ChangeBloom(bloomChangeTime, bloomStartValue, bloomvigEndValue));
            }
        }
    }

    IEnumerator ChangeVignette(float time, float start, float end)
    {
        while (time > 0)
        {
            vignette.center = new Vector2Parameter(vigCenter);
            time -= Time.deltaTime;
            vignette.intensity.value +=   (end - start) / time  * Time.deltaTime;
            yield return null;
        }
    }

    
    IEnumerator ChangeBloom(float time, float start, float end)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            bloom.intensity.value +=  (end - start) / time * Time.deltaTime;
            yield return null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
