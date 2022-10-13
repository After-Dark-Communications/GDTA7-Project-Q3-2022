using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;
using Collisions;
using ShipParts.Ship;

public abstract class Pickup : MonoBehaviour, ICollidable
{
    [SerializeField]
    private float pickupLiveTime;

    private TimeTracker timeTrack;

    
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
        Destroy(this.gameObject);
        Debug.Log("Destroy called");
    }

    // to handle collisions with other things
    public void HandleCollision<T1>(T1 objectThatHit, ShipStats shipStats) where T1 : ICollidable
    {

        //if (objectThatHit is ShipCollision)
        //{
        //    Debug.Log("Ship collision");

        //}

    }

    public virtual void PickUpAction(ShipBuilder shipBuilder) {

        DestroySelf();
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
       //DestroySelf();
    }


}
