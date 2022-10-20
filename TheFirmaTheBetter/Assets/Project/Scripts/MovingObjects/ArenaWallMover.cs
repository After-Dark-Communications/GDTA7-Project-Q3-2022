using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWallMover : MonoBehaviour
{
    [SerializeField]
    private List<Transform> transforms;

    private int currentTargetIndex;

    

    private void Update()
    {
        Transform currentTarget = transforms[currentTargetIndex];

        float distance = Vector3.Distance(currentTarget.position, transform.position);

        if (distance > 0)
            return;
    }
}
