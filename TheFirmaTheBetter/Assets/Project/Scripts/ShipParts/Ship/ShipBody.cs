using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ShipParts.Ship
{
    public class ShipBody : MonoBehaviour
    {
        public UnityCollisionEvent OnPlayerCrash = new UnityCollisionEvent();
        public UnityCollisionEvent OnPlayerLeaveCrash = new UnityCollisionEvent();
        public void OnCollisionEnter(Collision collision)
        {
            OnPlayerCrash.Invoke(collision.impulse, collision.gameObject);
        }

        public void OnCollisionExit(Collision collision)
        {
            OnPlayerLeaveCrash.Invoke(collision.impulse, collision.gameObject);
        }
    }
}
