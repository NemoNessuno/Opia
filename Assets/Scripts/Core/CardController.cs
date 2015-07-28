using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class CardController : MonoBehaviour
{    
    public Action OnRemoveAnimationEnded;
 
    public void AfterAnimation()
    {        
        OnRemoveAnimationEnded();
    }

    public void PlayRemoveAnimation()
    {
        var animations = GetComponent<Animation>();

        foreach (AnimationState anim in animations)
        {
            animations.Play(anim.name);
        }        
    }

    void OnMouseDown()
    {
        PlayRemoveAnimation();
    }
}