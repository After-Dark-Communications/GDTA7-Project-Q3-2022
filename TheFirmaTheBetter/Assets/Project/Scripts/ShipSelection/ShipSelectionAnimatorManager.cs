using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipSelection
{
    
    public class ShipSelectionAnimatorManager : MonoBehaviour
    {
        private const string BOOLEAN_SHIPCOMPLETED_NAME = "ShipCompleted";
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayShipCompletedAnimation()
        {
            animator.SetBool(BOOLEAN_SHIPCOMPLETED_NAME, true);
        }
    }
}
