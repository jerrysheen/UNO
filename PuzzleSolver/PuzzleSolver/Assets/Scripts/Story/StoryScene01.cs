using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StoryManagement
{
    /// <summary>
    /// 新的故事要注册到story中去，这边用的是enum 标记， 那边则是数字？
    /// </summary>
    public enum StoryLine01
    {
        Click_Scroll = 1,
        Click_Cinnabar = 2,
        Leave_FingerPrint = 3
    }

    public class StoryScene01 : Story
    {
        public override void GoToNext(int curr, int next)
        {
            base.GoToNext(curr, next);
        }
    }
    
}
