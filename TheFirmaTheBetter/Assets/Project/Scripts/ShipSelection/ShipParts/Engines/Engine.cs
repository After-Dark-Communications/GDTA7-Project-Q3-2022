using Collisions;
using EventSystem;
using Managers;
using ShipParts.Ship;
using ShipSelection.ShipBuilders.ConnectionPoints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Util;

namespace ShipParts.Engines
{
    [AddComponentMenu("Parts/Engine")]
    public class Engine : Part
    {
        private const float _minVibrateAmount = 0.01f, _maxVibrateAmount = 1f, _rumbleThreshold = 0.05f, _rumbleMultiplier = 0.875f;
        private const float _vibrateTime = 0.125f, _lowRumbleFreq = 0.5f, _highRumbleFreq = 0.5f;
        [SerializeField]
        private EngineData engineData;
        [SerializeField]
        private float HeightTime = 0.125f, HeightDifference = 5f, HeightSpeed = 7f;

        private float _throttle;
        private Vector2 _moveValue;
        private bool _changingHeight;
        private float _maxSpeed = 1f;

        private Vector3 _lastPosition;

        protected override void Setup()
        {
            //set the evenets
            if (rootInputHandler != null)
            {
                rootInputHandler.OnPlayerMove.AddListener(MoveShip);
                shipRoot.GetComponent<ShipBody>().OnPlayerCrash.AddListener(CrashShip);
                rootInputHandler.OnPlayerMoveUp.AddListener(MoveUp);
                rootInputHandler.OnPlayerMoveDown.AddListener(MoveDown);
            }
            //determine unaltered max speed
            _maxSpeed = engineData.Speed / shipRigidBody.drag;

            _lastPosition = shipRigidBody.position;
        }

        private void Update()
        {
            if (rootInputHandler == null)
                return;

            if (_moveValue != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(_moveValue.x, 0, _moveValue.y), GlobalUp.UP.up);
                shipRoot.rotation = Quaternion.RotateTowards(shipRoot.rotation, toRotation, engineData.Handling * Time.deltaTime);

                GetComponentInParent<PlayerStatistics>().DistanceTravelled += shipRigidBody.velocity.magnitude * Time.deltaTime;
            }
        }

        private void FixedUpdate()
        {
            if (rootInputHandler == null)
                return;

            Vector3 forward = shipRoot.transform.forward;
            forward.y = 0;
            shipRigidBody.AddForce(forward.normalized * _throttle * (engineData.Speed * Time.fixedDeltaTime), ForceMode.Impulse);
        }

        private void MoveShip(Vector2 move)
        {//when starting to move, increase T and lerp towards top speed
         //when stopping, decrease T and lerp towards 0 speed
            _throttle = new Vector3(move.x, 0, move.y).magnitude;
            _moveValue = move;
            ShipBuilder shipBuilder = transform.GetComponentInParent<ShipBuilder>();

            if (shipBuilder == null)
                return;

            Channels.Movement.OnShipMove?.Invoke(move, shipBuilder.PlayerNumber);
        }

        private void MoveUp(ButtonStates arg0)
        {
            if (!_changingHeight)
            {
                StartCoroutine(ChangeYForTime(HeightTime, HeightDifference, HeightSpeed));
            }
        }

        private void MoveDown(ButtonStates arg0)
        {
            if (!_changingHeight)
            {
                StartCoroutine(ChangeYForTime(HeightTime, -HeightDifference, HeightSpeed));
            }
        }

        private void CrashShip(Vector3 velocity, GameObject other)
        {
            if (velocity.magnitude >= _rumbleThreshold)
            {//start rumble
                float crashVelocity = velocity.magnitude; //Mathf.Abs(Vector3.Dot(velocity, ShipRoot.forward));
                                                          //float velocity = rb.velocity.sqrMagnitude;
                                                          //Debug.Log($"{ShipRoot.name} velocity: {crashVelocity} Remapped to {crashVelocity.Remap(0, _maxSpeed, _minVibrateAmount, _maxVibrateAmount)}");
                float rumbleStrenght = crashVelocity.Remap(0, _maxSpeed, _minVibrateAmount, _maxVibrateAmount) * _rumbleMultiplier;
                //UnityEngine.InputSystem.Gamepad.current.SetMotorSpeeds(0, rumbleStrenght);
                StartCoroutine(VibrateForTime(_vibrateTime, rumbleStrenght * _lowRumbleFreq, rumbleStrenght * _highRumbleFreq));
            }
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

        public override PartData GetData()
        {
            return engineData;
        }

        private IEnumerator VibrateForTime(float time, float low, float high)
        {
            UnityEngine.InputSystem.Gamepad gamepad = myInputDevice as UnityEngine.InputSystem.Gamepad;
            //activate rumble
            gamepad?.SetMotorSpeeds(low, high);
            yield return new WaitForSecondsRealtime(time);
            //stop rumble
            gamepad?.SetMotorSpeeds(0, 0);
        }

        private IEnumerator ChangeYForTime(float time, float height, float speed)
        {
            _changingHeight = true;
            Vector3 origin = shipRoot.transform.position;
            float posy = origin.y;
            float t = 0;
            while (t < 1)
            {
                LerpToHeight(1);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(time);
            t = 1;
            while (t > 0)
            {
                LerpToHeight(-1);
                yield return new WaitForEndOfFrame();
            }
            shipRoot.transform.position = new Vector3(shipRoot.transform.position.x, origin.y, shipRoot.transform.position.z);
            _changingHeight = false;

            void LerpToHeight(int TimeMultiplier)
            {
                posy = Mathf.Lerp(origin.y, origin.y + height, t);
                shipRoot.transform.position = new Vector3(shipRoot.transform.position.x, posy, shipRoot.transform.position.z);
                t += (Time.deltaTime * speed) * TimeMultiplier;
            }
        }

        private void OnDisable()
        {
            UnityEngine.InputSystem.Gamepad gamepad = myInputDevice as UnityEngine.InputSystem.Gamepad;

            if (gamepad == null)
                return;

            gamepad.SetMotorSpeeds(0, 0);
        }

        public override string PartCategoryName => "Engine";

        public EngineData EngineData => engineData;
    }
}