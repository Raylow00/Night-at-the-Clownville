using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetTriggerOnEnable : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string trigger;

    void OnEnable()
    {
        animator.SetTrigger(trigger);
    }
}
