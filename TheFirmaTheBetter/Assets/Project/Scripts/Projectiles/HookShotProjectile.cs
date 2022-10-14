using Projectiles;
using ShipParts.Ship;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public class HookShotProjectile : Projectile
    {
        private const float HookConnectedTime = 10f;//4f;

        private float currentConnectedTime = 0;
        private float currentArmingTime = 0;

        private bool armed = false;

        private ShipBuilder target;//object that is hit
        //private Rigidbody firerer;
        private Rigidbody _firerer;

        private float _desiredSegmentLength = 0.25f;//must be positive
        private float  _initialSegmentLength = 1f, _currentSegmentLength = 1f;
        private Vector3 _ropeDirection;

        private LineRenderer _lineRenderer;
        private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
        private const float _toDesiredLengthTime = HookConnectedTime / 2f;
        private const int _segmentCount = 35;
        private const float _lineWidth = 0.1f;
        private const float _rotateTime = 10f;
        private const int _constraintSimulations = 50;//higher should get better results
        private const float _pullDelay = 1f;//should be less than HookConnectedTime

        private void Awake()
        {
            //joint = GetComponent<SpringJoint>();
            _lineRenderer = GetComponent<LineRenderer>();

            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
        }

        public void SetJointFirer(Rigidbody shipbuilder)
        {
            _firerer = shipbuilder;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (armed == false)
                return;

            ShipBuilder shipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();

            if (shipBuilder == null)
                return;

            //joint.connectedBody = _firerer;
            target = shipBuilder;
            Vector3 ropeStart = _firerer.position;
            Vector3 diff = (_firerer.position - target.transform.parent.position);
            _initialSegmentLength = Mathf.Abs(diff.magnitude) / _segmentCount;
            _currentSegmentLength = _initialSegmentLength;
            for (int i = 0; i < _segmentCount; i++)
            {
                _ropeSegments.Add(new RopeSegment(ropeStart));
                ropeStart -= diff.normalized * _currentSegmentLength;
            }
            //_ropeSegments[^1] = new RopeSegment(_firerer.position);

        }

        private void Update()
        {
            ArmHook();
            if (target == null)
                return;
            _currentSegmentLength = Mathf.Lerp(_initialSegmentLength, _desiredSegmentLength, -_pullDelay + currentConnectedTime);
            FollowHookShotWithTarget();

            currentConnectedTime += Time.deltaTime;
            if (currentConnectedTime < HookConnectedTime)
                return;

            Destroy(transform.parent.gameObject);
        }

        private void FixedUpdate()
        {
            if (target == null)
            { return; }
            Simulate();
        }

        private void LateUpdate()
        {
            if (target == null)
            { return; }

            //lerp rotation of attached object
            Vector3 lookPos = _firerer.position;//_ropeSegments[_segmentCount - 2].CurrentPos;
            lookPos.y = 0;
            target.transform.parent.rotation = Quaternion.Lerp(target.transform.parent.rotation, Quaternion.LookRotation(lookPos), _rotateTime * Time.deltaTime);
            //adjust position of attached object
            target.transform.parent.position = _ropeSegments[_segmentCount - 1].CurrentPos;
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

        private void FollowHookShotWithTarget()
        {
            if (target == null)
                return;

            Vector3 dir = (_firerer.position - target.transform.parent.position);
            _ropeDirection = new Vector3(dir.x * -1, dir.y, dir.z * -1);
            DrawRope();
        }

        private void Simulate()
        {
            _lineRenderer.positionCount = _segmentCount;

            //simulate movements
            for (int i = 0; i < _segmentCount; i++)
            {
                RopeSegment thisSegment = _ropeSegments[i];
                Vector3 velocity = thisSegment.CurrentPos - thisSegment.OldPos;
                thisSegment.OldPos = thisSegment.CurrentPos;
                thisSegment.CurrentPos += velocity;
                thisSegment.CurrentPos += _ropeDirection * Time.fixedDeltaTime;
                _ropeSegments[i] = thisSegment;

            }
            //apply constraints
            for (int i = 0; i < _constraintSimulations; i++)
            {
                ApplyConstraints();
            }
        }

        private void ApplyConstraints()
        {
            //first segment is always connected to transform
            RopeSegment firstSegment = _ropeSegments[0];
            firstSegment.CurrentPos = _firerer.position;
            _ropeSegments[0] = firstSegment;

            //all other segments will be within a certain distance to it
            for (int i = 0; i < _segmentCount - 1; i++)
            {
                RopeSegment segmentStart = _ropeSegments[i];
                RopeSegment segmentEnd = _ropeSegments[i + 1];

                float dist = (segmentStart.CurrentPos - segmentEnd.CurrentPos).magnitude;
                float error = dist - _currentSegmentLength;
                Vector3 changeDir = (firstSegment.CurrentPos - segmentEnd.CurrentPos).normalized;
                Vector3 changeAmount = changeDir * error;

                if (i != 0)
                {
                    segmentStart.CurrentPos -= changeAmount * 0.5f;
                    _ropeSegments[i] = segmentStart;
                    segmentEnd.CurrentPos += changeAmount * 0.5f;
                    _ropeSegments[i + 1] = segmentEnd;
                }
                else
                {
                    segmentEnd.CurrentPos += changeAmount;
                    _ropeSegments[i + 1] = segmentEnd;
                }
            }
        }

        private void DrawRope()
        {
            Vector3[] ropePositions = new Vector3[_segmentCount];
            for (int i = 0; i < _segmentCount; i++)
            {
                ropePositions[i] = _ropeSegments[i].CurrentPos;
            }
            _lineRenderer.SetPositions(ropePositions);
            //Debug.Break();
        }

        public struct RopeSegment
        {
            public Vector3 CurrentPos, OldPos;

            public RopeSegment(Vector3 pos)
            {
                CurrentPos = pos;
                OldPos = pos;
            }
        }
    }
}