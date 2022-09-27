using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShipParts.Ship
{
    public class ShipBody : MonoBehaviour
    {
        public UnityVector3Event OnPlayerCrash = new UnityVector3Event();

        public void OnCollisionEnter(Collision collision)
        {
            //Debug.Log($"{collision.impulse.magnitude}");
            OnPlayerCrash.Invoke(collision.impulse);
        }
    }
}
