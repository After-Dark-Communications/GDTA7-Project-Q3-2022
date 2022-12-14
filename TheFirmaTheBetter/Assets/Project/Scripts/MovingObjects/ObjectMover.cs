using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using UnityEngine.AI;

namespace MovingObjects
{
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
            currentMovementPoint.Spawn();
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
            Channels.OnEnergyZoneMoved?.Invoke();
            Channels.Announcer.OnPlayEnergyZoneMoved?.Invoke();
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
}