using UnityEngine;

public class Arrow : MonoBehaviour
{
    private const string TRIGGER_NAME = "SelectArrow";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    public void PlaySelectedAnimation()
    {
        animator.SetTrigger(TRIGGER_NAME);
    }
}
