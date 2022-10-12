using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;
using Collisions;
using ShipParts.Ship;

public class PickupManager : MonoBehaviour, ICollidable
{
    [SerializeField]
    private float pickupLiveTime;

    private TimeTracker timeTrack;


    private void OnTriggerEnter(Collider other)
    {
        ICollidable collisionObject = other.GetComponentInParent<ICollidable>();

        if (collisionObject != null)
        {
            if (collisionObject is Ship)
                return;

            //impactSpawner.SpawnImpactHitPrefab();
            collisionObject.HandleCollision(this, null);
            Debug.Log("Collision");
        }
    }
    public void DestroySelf()
    {
        throw new System.NotImplementedException();
    }

    public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        timeTrack = new TimeTracker(pickupLiveTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeTrack.TimerComplete())
        {
            return;
        }
        Destroy(this.gameObject);
    }

}
