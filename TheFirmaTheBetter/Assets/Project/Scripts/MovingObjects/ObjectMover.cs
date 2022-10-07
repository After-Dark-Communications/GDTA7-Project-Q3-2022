using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectMover : MonoBehaviour
{
    private List<MovementPoint> movementPoints = new List<MovementPoint>();

    private int currentMovementPointIndex = 0;

    private MovementPoint currentMovementPoint => movementPoints[currentMovementPointIndex];

    private MovementPoint nextMovementPoint;

    private void Start()
    {
        MovementPoint[] movementPoints = transform.GetComponentsInChildren<MovementPoint>();

        foreach (MovementPoint point in movementPoints)
        {
            this.movementPoints.Add(point);
        }
        StartCorountineMovePoint();
    }

    public void StartCorountineMovePoint()
    {
        StartCoroutine(MoveToNextPoint());
    }

    IEnumerator MoveToNextPoint()
    {
        MovementPoint oldPoint = currentMovementPoint;

        ChangeIndex();

        nextMovementPoint = currentMovementPoint;

        oldPoint.DeSpawn();
        yield return new WaitForSeconds(10f);
        nextMovementPoint.Spawn();

    }

        void IncrementIndex()
        {
            currentMovementPointIndex++;

            if (currentMovementPointIndex >= movementPoints.Count)
                currentMovementPointIndex = 0;
        }

        void ChangeIndex()
        {
            currentMovementPointIndex = Random.Range(0, movementPoints.Count);
        }

    public MovementPoint NextMovementPoint { get { return nextMovementPoint; } }

}
