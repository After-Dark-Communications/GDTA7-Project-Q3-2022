using System;
using Assets.Scripts.ShipSelection.ShipBuilder.ConnectionPoints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Parts
{
    [AddComponentMenu("Parts/Engine")]
    public class Engine : Part
    {
        [SerializeField]
        private EngineData engineData;
        public override string PartCategoryName => "Engine";
        private Rigidbody rb;
        private float throttle;
        private Vector2 MoveValue;

        //IF YOU OVERRIDE PART'S AWAKE, BE SURE TO USE BASE.AWAKE() SO THAT IT HAS KNOWLEDGE OF ITS ROOT

        public override void Setup()
        {
            //set the evenets
            if (RootInputHanlder != null)
            {
                RootInputHanlder.OnPlayerMove.AddListener(MoveShip);
            }
            //get components from root
            rb = ShipRoot.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (MoveValue != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(MoveValue.x, 0, MoveValue.y), GlobalUp.UP.up);
                ShipRoot.rotation = Quaternion.RotateTowards(ShipRoot.rotation, toRotation, engineData.Handling * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            Vector3 forward = ShipRoot.transform.forward;
            forward.y = 0;
            //rb.velocity = forward.normalized * throttle * (engineData.Speed * Time.fixedDeltaTime);
            rb.AddForce(forward.normalized * throttle * (engineData.Speed * Time.fixedDeltaTime), ForceMode.Impulse);
        }

        private void MoveShip(Vector2 move)
        {//when starting to move, increase T and lerp towards top speed
         //when stopping, decrease T and lerp towards 0 speed
            throttle = new Vector3(move.x, 0, move.y).magnitude;
            MoveValue = move;
            //ShipRoot.transform.position += forward.normalized * throttle * (engineData.Speed * Time.deltaTime);

        }

        public override bool IsMyConnectionType(ConnectionPoint connectionPoint)
        {
            if (connectionPoint is EngineConnectionPoint)
                return true;

            return false;
        }

        public override bool IsMyType(Part part)
        {
            if (part is Engine)
                return true;

            return false;
        }
    }
}