using MovingObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zones
{
    public class EnergyZoneSpawningBehaviour : StateMachineBehaviour
    {
        ObjectMover objectMover;
        const float lifeTimeParticle = 5f;
        MovementPoint nextMovementPoint;
        [SerializeField]
        private GameObject spawnParticle;

        protected GameObject clone;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            objectMover = animator.GetComponentInParent<ObjectMover>();

            nextMovementPoint = objectMover.NextMovementPoint;

            clone = Instantiate(spawnParticle, nextMovementPoint.transform.position, nextMovementPoint.transform.rotation);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Destroy(clone);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}