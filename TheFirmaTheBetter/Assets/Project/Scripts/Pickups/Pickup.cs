using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;
using Collisions;
using ShipParts.Ship;
using EventSystem;

public abstract class Pickup : MonoBehaviour, ICollidable
{
    // --> if destroying pickups
    //[SerializeField]
    //private float pickupLiveTime;

    //private TimeTracker timeTrack;

    [SerializeField]
    private GameObject pickedUpParticle;

    private PickupZoneSpawnManager manager;
   
    private void OnTriggerEnter(Collider other)
    {
        ICollidable collisionObject = other.GetComponentInParent<ICollidable>();

        if (collisionObject is null)
            return;

        ShipBuilder shipBuilder = other.GetComponentInParent<ShipBuilder>();

        if (shipBuilder is null)
            return ;

        PickUpAction(shipBuilder);
            Debug.Log("Collision");
        
    }
    public void DestroySelf()
    {
        manager = GetComponentInParent<PickupZoneSpawnManager>();
        Channels.OnPickupDestroyed?.Invoke();
        manager.AdjustSpawnedCount();
        Destroy(this.gameObject);
        Debug.Log("Destroy called");
    }

    // to handle collisions with other things
    public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable { }

    public virtual void PickUpAction(ShipBuilder shipBuilder) {

        Instantiate(pickedUpParticle,transform.position,Quaternion.identity);
        DestroySelf();
    }
    private void Awake()
    {
        // --> if destroying pickups
        // timeTrack = new TimeTracker(pickupLiveTime);

       

    }

    void Update()
    {
        // --> if destroying pickups
        //if (!timeTrack.TimerComplete())
        //{
        //    return;
        //}
        //DestroySelf();
    }


}
