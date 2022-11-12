using System.Collections;
using System.Collections.Generic;
using StoryManagement;
using UnityEngine;

public class Scene01BGM : UIControllBase
{
    // Start is called before the first frame update
    void Start()
    {
          AudioManager.BgmaudioSource.clip = base.clip;
          AudioManager.BgmaudioSource.loop = base.loop;
          AudioManager.BgmaudioSource.volume = base.volume;
          
          AudioManager.BgmaudioSource.Play((ulong)base.delay);
    }

    // Update is called once per frame
    void Update()
    {

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
            AudioManager.BgmaudioSource.clip = base.clip;
            AudioManager.BgmaudioSource.Play();
        }
    }
}
