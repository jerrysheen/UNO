using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using UnityEngine;

public class Scene04CharacterAnimationSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationClip idle;
    public AnimationClip move;

    public Animation charecterAnimation;
    void Start()
    { 
        
        charecterAnimation = this.transform.Find("Show").GetComponent<Animation>();
        charecterAnimation.clip = idle;
    }

    // Update is called once per frame
    void Update()
    {
        var charecterState = this.GetComponent<PathFollower>().currState;
        if (charecterState == PathFollower.CharacterMoveState.Idle)
        {
            charecterAnimation.clip = idle;
            charecterAnimation.Play(idle.name);
        }
        else
        {
            charecterAnimation.clip = move;
            charecterAnimation.Play(move.name);
        }

    }
}
