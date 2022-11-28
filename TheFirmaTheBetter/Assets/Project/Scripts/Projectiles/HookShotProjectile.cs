using EventSystem;
using Projectiles;
using ShipParts.Engines;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Projectiles
{
    public class HookShotProjectile : Projectile
    {
        private const int _ignoreCastLayer = 2;//ignoreraycast layer
        private const float _hookConnectedTime = 10f;//4f;
        private const float _maxAddedVelocity = 50f;

        private float currentConnectedTime = 0;
        private float currentArmingTime = 0;

        private bool armed = false;

        private ShipBuilder target;//object that is hit
        //private Rigidbody firerer;
        private Rigidbody _firerer;

        private const float _desiredSegmentLength = .3f;//must be positive
        private float _initialSegmentLength = 1f, _currentSegmentLength = 1f;
        private Vector3 _ropeGravity;


        private LineRenderer _lineRenderer;
        public RopeNode[] _ropeSegments;
        private const int _segmentCount = 35;
        private const float _lineWidth = 0.25f;
        private const float _rotateTime = 10f;
        private const float _stepTime = 0.01f;
        private const int _constraintSimulations = 100;//higher should get better results
        private const float _pullDelay = 1f;//should be less than HookConnectedTime

#if UNITY_EDITOR
        private Vector3 GizmoPosition;
#endif

        private void Awake()
        {
            //joint = GetComponent<SpringJoint>();
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;

            Channels.OnPlayerBecomesDeath += OnTargetOrFirerDies;
            Channels.OnRoundOver += OnRoundEnd;
        }

        private void OnDestroy()
        {
            Channels.OnPlayerBecomesDeath -= OnTargetOrFirerDies;
            Channels.OnRoundOver -= OnRoundEnd;
        }

        private void OnTargetOrFirerDies(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller)
        {
            if (target == null)
            { return; }
            if (shipBuilderThatNeedsDying == target)
            {
                Destroy(transform.parent.gameObject);
            }
            else if (shipBuilderThatNeedsDying.PlayerNumber == FirerId)
            {
                Destroy(transform.parent.gameObject);
            }
        }
        private void OnRoundEnd(int roundIndex, int winnerIndex)
        {
            Destroy(transform.parent.gameObject);
        }

        public void SetJointFirer(Rigidbody shipbuilder)
        {
            _firerer = shipbuilder;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (armed == false)
                return;

            //new target got
            ShipBuilder shipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();
            if (shipBuilder == null)
                return;
            //set up rope
            transform.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //joint.connectedBody = _firerer;
            target = shipBuilder;

            Vector3 ropeStart = _firerer.position;
            Vector3 diff = (_firerer.position - target.transform.parent.position);
            diff.y = 0;//prevent height flaws
            _initialSegmentLength = Mathf.Abs(diff.magnitude) / _segmentCount;
            _currentSegmentLength = _initialSegmentLength;
            //set up ropenodes
            _lineRenderer.positionCount = _segmentCount;
            _ropeSegments = new RopeNode[_segmentCount];
            for (int i = 0; i < _segmentCount; i++)
            {
                _ropeSegments[i] = new RopeNode(ropeStart);
                ropeStart -= diff.normalized * _currentSegmentLength;
            }
            target.transform.parent.position = _ropeSegments[_segmentCount - 1].position;
            transform.parent.GetChild(1)?.gameObject.SetActive(false);
            //_ropeSegments[^1] = new RopeSegment(_firerer.position);
            _ropeGravity = -(_firerer.position - target.transform.parent.position).normalized;
            _ropeGravity.y = 0;
        }

        private void Update()
        {
            ArmHook();
            if (target == null)
                return;
            //TODO: try better lerping
            _currentSegmentLength = Mathf.Lerp(_initialSegmentLength, _desiredSegmentLength, -_pullDelay + currentConnectedTime);
            //Mathf.Lerp(_currentSegmentLength, _desiredSegmentLength, _toDesiredLengthSpeed * Time.deltaTime);
            //FollowHookShotWithTarget();

            currentConnectedTime += Time.deltaTime;
            if (currentConnectedTime < _hookConnectedTime)
                return;

            Destroy(transform.parent.gameObject);
        }

        private void FixedUpdate()
        {
            if (target == null)
            { return; }
            //Simulate();
            SimulateVerlet();
            for (int i = 0; i < _constraintSimulations; i++)
            {
                ApplyVerletConstraints();
            }
            Vector3 lookPos = _firerer.position;//_ropeSegments[_segmentCount - 2].CurrentPos;
            lookPos.y = 0;
            Rigidbody targetRigidbody = target.transform.parent.GetComponent<Rigidbody>();
            target.transform.parent.rotation = Quaternion.Lerp(target.transform.parent.rotation, Quaternion.LookRotation(lookPos), _rotateTime * Time.deltaTime);
            //adjust position of attached object
            //TODO: issue where, if the hooked ship is too far away from where it should be, the hooked ship will jolt towards the firerer.
            Debug.DrawRay(_ropeSegments[^1].prevPosition, (_ropeSegments[^1].position - _ropeSegments[^1].prevPosition) * 5);

            Vector3 desiredVelocity = (_ropeSegments[^1].position - _ropeSegments[^1].prevPosition) / Time.fixedDeltaTime;
            //Debug.Log($"added velocity:{desiredVelocity}, current velocity:{target.transform.parent.GetComponent<Rigidbody>().velocity}");
            target.transform.parent.GetComponent<Rigidbody>().velocity = desiredVelocity.Clamp(-_maxAddedVelocity, _maxAddedVelocity);
            _ropeSegments[^1].position = target.transform.parent.position;
            DrawRope();
        }

        public void ArmHook()
        {
            if (armed)
                return;

            currentArmingTime += Time.deltaTime;

            if (currentArmingTime < ProjectileData.ArmingTime)
                return;

            armed = true;
        }

        private void SimulateVerlet()
        {
            //original source https://toqoz.fyi/game-rope.html
            for (int i = 0; i < _ropeSegments.Length; i++)
            {
                RopeNode node = _ropeSegments[i];
                StepVerlet(node);
            }

            void StepVerlet(RopeNode node)
            {
                // NOTE: cur_pos - old_pos is not actual velocity.  To calculate real velocity, use (cur_pos - old_pos) / dt
                float deltaTime = _stepTime;//Time.fixedDeltaTime;
                Vector3 temp = node.position;
                node.position += (node.position - node.prevPosition) + _ropeGravity * (deltaTime * deltaTime);
                //Debug.DrawRay(node.position, (node.position - node.prevPosition).normalized * 2f, Color.green);
                node.prevPosition = temp;
            }

        }

        private void ApplyVerletConstraints()
        {
            // Distance constraint which reduces iterations, but doesn't handle stretchyness in a natural way.
            RopeNode firstNode = _ropeSegments[0];
            RopeNode lastNode = _ropeSegments[^1];
            float distance = Vector3.Distance(firstNode.position, lastNode.position);
            if (distance > 0 && distance > _ropeSegments.Length * _currentSegmentLength)
            {
                Vector3 dir = (lastNode.position - firstNode.position).normalized;
                lastNode.position = firstNode.position + _ropeSegments.Length * _currentSegmentLength * dir;
            }

            for (int i = 0; i < _ropeSegments.Length - 1; i++)
            {
                RopeNode node1 = _ropeSegments[i];
                RopeNode node2 = _ropeSegments[i + 1];

                if (i == 0)
                {
                    node1.position = _firerer.position;
                }
                float diffX = node1.position.x - node2.position.x;
                float diffZ = node1.position.z - node2.position.z;
                float dist = Vector3.Distance(node1.position, node2.position);
                float difference = 0;
                //divide-by-zero prevention
                if (dist > 0)
                {
                    difference = (_currentSegmentLength - dist) / dist;
                }

                Vector3 translate = new Vector3(diffX, 0, diffZ) * (0.5f * difference);
                node1.position += translate;
                node2.position -= translate;
            }
        }

        private void DrawRope()
        {
            Vector3[] ropePositions = new Vector3[_segmentCount];
            for (int i = 0; i < ropePositions.Length; i++)
            {
                ropePositions[i] = _ropeSegments[i].position;
            }
            _lineRenderer.SetPositions(ropePositions);
            //Debug.Break();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            { return; }
            if (_ropeSegments == null)
            { return; }
            for (int i = 0; i < _ropeSegments.Length - 1; i++)
            {
                if (i % 2 == 0)
                {
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawLine(_ropeSegments[i].position, _ropeSegments[i + 1].position);
            }
            Gizmos.DrawWireSphere(GizmoPosition, 5 / 2);
        }
#endif
    }
    public class RopeNode
    {
        public Vector3 position, prevPosition;

        public RopeNode(Vector3 pos)
        {
            position = pos;
            prevPosition = pos;
        }
    }
}