using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    const string animationName = "Selected";

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    public void PlaySelectedAnimation()
    {
        animator.Play(animationName);
    }
}
