using Collisions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageManager : MonoBehaviour
{
    private const string HideMessageBool = "Hide";

    [SerializeField]
    private Animator tutorialMessageAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ShipCollision>() != null)
        {
            if (tutorialMessageAnimator.gameObject.activeSelf == false)
            {
                tutorialMessageAnimator.gameObject.SetActive(true);
                return;
            }

            tutorialMessageAnimator.SetBool(HideMessageBool, false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<ShipCollision>() != null)
        {
            tutorialMessageAnimator.SetBool(HideMessageBool, true);
        }
    }
}
