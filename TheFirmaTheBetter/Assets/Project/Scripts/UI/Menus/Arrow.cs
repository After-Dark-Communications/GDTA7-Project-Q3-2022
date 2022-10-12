using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const string triggerName = "SelectArrow";

    private Animator animator;

    //Animator animator;

    //[SerializeField]
    //const string animationName = "Selected";

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    private void OnEnable()
    {
        Channels.OnSelectedCategoryChanged += OnCategoryChanged;
    }

    private void OnDisable()
    {
        Channels.OnSelectedCategoryChanged -= OnCategoryChanged;
    }

    private void OnCategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
    {
        
    }

    public void PlaySelectedAnimation()
    {
        animator.SetTrigger(triggerName);

        //animator.Play(animationName);
    }
}
