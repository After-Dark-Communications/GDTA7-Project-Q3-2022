using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRandomizer : MonoBehaviour
{
    private const int minRockAnimations = 0;
    private const int maxRockAnimations = 4;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        int randAnimationIndex = Random.Range(minRockAnimations, maxRockAnimations);
        animator.SetInteger("Move", randAnimationIndex);
    }
}
