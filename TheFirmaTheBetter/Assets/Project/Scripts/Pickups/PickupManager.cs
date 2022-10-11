using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickups;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    private float pickupLiveTime;

    private TimeTracker timeTrack;


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
        Debug.Log("Destroyed");
        Destroy(this.gameObject);
    }

}
