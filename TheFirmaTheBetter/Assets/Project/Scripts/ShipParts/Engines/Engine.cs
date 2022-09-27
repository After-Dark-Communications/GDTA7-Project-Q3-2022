using ShipSelection.ShipBuilder.ConnectionPoints;
using Parts;
using System;
//using  ShipSelection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using ShipParts.Ship;

namespace Parts
{
    [AddComponentMenu("Parts/Engine")]
    public class Engine : Part
    {
        private const float _minVibrateAmount = 0.01f, _maxVibrateAmount = 1f;
        [SerializeField]
        private EngineData engineData;
        [SerializeField]
        private float VibrateTime = 0.1f;
        [SerializeField]
        private float HeightTime = 0.125f, HeightDifference = 5f, HeightSpeed = 7f;

        private float throttle;
        private Vector2 MoveValue;
        private bool ChangingHeight;

        protected override void Setup()
        {
            //set the evenets
            if (RootInputHandler != null)
            {
                RootInputHandler.OnPlayerMove.AddListener(MoveShip);
                ShipRoot.GetComponent<ShipBody>().OnPlayerCrash.AddListener(CrashShip);
                RootInputHandler.OnPlayerMoveUp.AddListener(MoveUp);
                RootInputHandler.OnPlayerMoveDown.AddListener(MoveDown);
            }
            //get components from root
        }

        private void Update()
        {
            if (RootInputHandler == null)
                return;

            if (MoveValue != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(MoveValue.x, 0, MoveValue.y), GlobalUp.UP.up);
                ShipRoot.rotation = Quaternion.RotateTowards(ShipRoot.rotation, toRotation, engineData.Handling * Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (RootInputHandler == null)
                return;

            Vector3 forward = ShipRoot.transform.forward;
            forward.y = 0;
            ShipRigidBody.AddForce(forward.normalized * throttle * (engineData.Speed * Time.fixedDeltaTime), ForceMode.Impulse);
        }

        private void MoveShip(Vector2 move)
        {//when starting to move, increase T and lerp towards top speed
         //when stopping, decrease T and lerp towards 0 speed
            throttle = new Vector3(move.x, 0, move.y).magnitude;
            MoveValue = move;

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


        private void CrashShip(Vector3 velocity)
        {
            //Rigidbody rb = ShipRoot.GetComponent<Rigidbody>();
            float crashVelocity = velocity.magnitude; //Mathf.Abs(Vector3.Dot(velocity, ShipRoot.forward));
            //float velocity = rb.velocity.sqrMagnitude;
            //Debug.Log($"{ShipRoot.name} velocity: {crashVelocity} Remapped to {crashVelocity.Remap(0, engineData.Speed, _minVibrateAmount, _maxVibrateAmount)}");
            //UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(0, velocity.Remap(0, engineData.Speed, 0, 1));
            StartCoroutine(VibrateForTime(VibrateTime, 0, crashVelocity.Remap(0, engineData.Speed, _minVibrateAmount, _maxVibrateAmount)));
        }

        private IEnumerator VibrateForTime(float time, float low, float high)
        {
            //UnityEngine.InputSystem.PlayerInput playerInput = new UnityEngine.InputSystem.PlayerInput();
            //(playerInput.devices[1] as UnityEngine.InputSystem.Gamepad).SetMotorSpeeds(low, high);
            //TODO: change Gamepad.current to the bit above
            (MyInputDevice as UnityEngine.InputSystem.Gamepad).SetMotorSpeeds(low, high);
            //UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(low, high);
            yield return new WaitForSecondsRealtime(time);
            (MyInputDevice as UnityEngine.InputSystem.Gamepad).SetMotorSpeeds(0, 0);
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