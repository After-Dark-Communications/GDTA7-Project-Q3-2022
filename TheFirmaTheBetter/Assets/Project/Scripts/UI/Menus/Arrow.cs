using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const string TRIGGER_NAME = "SelectArrow";
    private const string BOOLEAN_SHIPCOMPLETED_NAME = "ShipCompleted";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    public void PlaySelectedAnimation()
    {
        animator.SetTrigger(TRIGGER_NAME);
    }

    public void PlayShipCompletedAnimation()
    {
        animator.SetBool(BOOLEAN_SHIPCOMPLETED_NAME, true);
    }
}
