using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovementPoint : MonoBehaviour
{
    private const string SpawnTriggerName = "Spawn";
    private const string DeSpawnTriggerName = "Despawn";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Spawn()
    {
        animator.SetTrigger(SpawnTriggerName);
        animator.enabled = true;
    }

    public void DeSpawn()
    {
        animator.SetTrigger(DeSpawnTriggerName);
        animator.enabled = true;
    }
}