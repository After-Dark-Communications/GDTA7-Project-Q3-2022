using  ShipSelection.ShipBuilder.ConnectionPoints;
using Parts;
using System;
//using  ShipSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Parts
{
    [AddComponentMenu("Parts/Engine")]
    public class Engine : Part
    {
        [SerializeField]
        private EngineData engineData;
        [SerializeField]
        private float VibrateTime = 1f;
        [SerializeField]
        private float HeightTime = 0.125f, HeightDifference = 5f, HeightSpeed = 7f;

        private Rigidbody rb;
        private float throttle;
        private Vector2 MoveValue;
        private bool ChangingHeight;

        //IF YOU OVERRIDE PART'S AWAKE, BE SURE TO USE BASE.AWAKE() SO THAT IT HAS KNOWLEDGE OF ITS ROOT

        public override void Setup()
        {
            //set the evenets
            if (RootInputHanlder != null)
            {
                RootInputHanlder.OnPlayerMove.AddListener(MoveShip);
                RootInputHanlder.OnPlayerCrash.AddListener(CrashShip);
                RootInputHanlder.OnPlayerMoveUp.AddListener(MoveUp);
                RootInputHanlder.OnPlayerMoveDown.AddListener(MoveDown);
            }
            //get components from root
            rb = ShipRoot.GetComponent<Rigidbody>();
        }



        private void Update()
        {
            if (RootInputHanlder == null)
                return;

            if (MoveValue != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(MoveValue.x, 0, MoveValue.y), GlobalUp.UP.up);
                ShipRoot.rotation = Quaternion.RotateTowards(ShipRoot.rotation, toRotation, engineData.Handling * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (RootInputHanlder == null)
                return;

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

        private void MoveUp(ButtonStates arg0)
        {
            if (!ChangingHeight)
            {
                StartCoroutine(ChangeYForTime(HeightTime, HeightDifference, HeightSpeed));
            }
        }

        private void MoveDown(ButtonStates arg0)
        {
            if (!ChangingHeight)
            {
                StartCoroutine(ChangeYForTime(HeightTime, -HeightDifference, HeightSpeed));
            }
        }


        private void CrashShip(float velocity)
        {
            //Rigidbody rb = ShipRoot.GetComponent<Rigidbody>();
            //float velocity = Mathf.Abs(Vector3.Dot(rb.velocity, ShipRoot.forward));
            //float velocity = rb.velocity.sqrMagnitude;
            Debug.Log($"{ShipRoot.name} velocity: {velocity} Remapped to {velocity.Remap(0, engineData.Speed, 0, 1)}");
            //UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(0, velocity.Remap(0, engineData.Speed, 0, 1));
            StartCoroutine(VibrateForTime(VibrateTime, 0, velocity.Remap(0, engineData.Speed, 0, 1)));
        }

        private IEnumerator VibrateForTime(float time, float low, float high)
        {
            //UnityEngine.InputSystem.PlayerInput playerInput = new UnityEngine.InputSystem.PlayerInput();
            //(playerInput.devices[1] as UnityEngine.InputSystem.Gamepad).SetMotorSpeeds(low, high);
            //TODO: change Gamepad.current to the bit above
            UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(low, high);
            yield return new WaitForSecondsRealtime(time);
            UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(0, 0);
        }

        private IEnumerator ChangeYForTime(float time, float height, float speed)
        {
            ChangingHeight = true;
            Vector3 origin = ShipRoot.transform.position;
            float posy = origin.y;
            float t = 0;
            while (t < 1)
            {
                posy = Mathf.Lerp(origin.y, origin.y + height, t);
                ShipRoot.transform.position = new Vector3(ShipRoot.transform.position.x, posy, ShipRoot.transform.position.z);
                t += Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(time);
            t = 1;
            while (t > 0)
            {
                posy = Mathf.Lerp(origin.y, origin.y + height, t);
                ShipRoot.transform.position = new Vector3(ShipRoot.transform.position.x, posy, ShipRoot.transform.position.z);
                t -= Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
            ShipRoot.transform.position = new Vector3(ShipRoot.transform.position.x, origin.y, ShipRoot.transform.position.z);
            ChangingHeight = false;
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
        
        public override string PartCategoryName => "Engine";
    }
}