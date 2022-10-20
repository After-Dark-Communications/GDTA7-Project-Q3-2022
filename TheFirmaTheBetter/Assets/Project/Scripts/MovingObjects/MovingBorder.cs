using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingBorder : MonoBehaviour
{
    [SerializeField]
    private GameObject movingBorder;
    private List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField]
    private int movingSpeed = 4;
    private int target;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = GetComponentsInChildren<Waypoint>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        movingBorder.transform.position = Vector3.MoveTowards(movingBorder.transform.position, waypoints[target].transform.position, movingSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(movingBorder.transform.position == waypoints[target].transform.position)
        {
            if (target == waypoints.Count - 1)
            {
                target = 0;
            }
            else
            {
                target ++;
            }
        }
    }
}
