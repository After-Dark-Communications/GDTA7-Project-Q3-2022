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
        private float _initialSegmentLength = 1f, _currentSegmentLength = 1f;
        private Vector3 _ropeDirection;

        private LineRenderer _lineRenderer;
        public RopeNode[] _ropeSegments;
        private const float _toDesiredLengthSpeed = 2f;
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
            _ropeSegments = new RopeNode[_segmentCount];
            for (int i = 0; i < _segmentCount; i++)
            {
                _ropeSegments[i] = new RopeNode(ropeStart);
                ropeStart -= diff.normalized * _currentSegmentLength;
            }
            target.transform.parent.position = _ropeSegments[_segmentCount - 1].position;
            transform.parent.GetChild(1)?.gameObject.SetActive(false);
            //_ropeSegments[^1] = new RopeSegment(_firerer.position);
            _ropeDirection = -(_firerer.position - target.transform.parent.position).normalized;
            _ropeDirection.y = 0;

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
            if (currentConnectedTime < HookConnectedTime)
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
            DrawRope();
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
            target.transform.parent.position = _ropeSegments[_segmentCount - 1].position;
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

        //private void FollowHookShotWithTarget()
        //{
        //    if (target == null)
        //        return;
        //
        //    //Vector3 dir = (_firerer.position - target.transform.parent.position).normalized;//-_firerer.transform.forward;//
        //    //dir.y = 0;
        //    //Debug.Break();
        //    //Debug.DrawLine(_firerer.position, (_firerer.position + dir)*10, Color.red);
        //    Debug.DrawRay(_firerer.position, _ropeDirection * 10, Color.red);
        //    DrawRope();
        //}


        private void SimulateVerlet()
        {
            for (int i = 0; i < _ropeSegments.Length; i++)
            {
                RopeNode node = _ropeSegments[i];
                Debug.Log($"node[{i}] prev{node.prevPosition}, cur{node.position}");
                StepVerlet(node);
            }
            Debug.Log("----------");

            void StepVerlet(RopeNode node)
            {
                // NOTE: cur_pos - old_pos is not actual velocity.  To calculate real velocity, use (cur_pos - old_pos) / dt
                float deltaTime = Time.fixedDeltaTime;
                Vector3 temp = node.position;
                node.position += (node.position - node.prevPosition) + _ropeDirection * deltaTime * deltaTime;
                //Debug.DrawRay(node.position, (node.position - node.prevPosition).normalized * 2f, Color.green);
                node.prevPosition = temp;
            }

        }

        private void ApplyVerletConstraints()
        {
            //TODO: try without this
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
        /*
        private void Simulate()
        {
            _lineRenderer.positionCount = _segmentCount;

            //simulate movements
            for (int i = 0; i < _segmentCount; i++)
            {
                RopeNode thisSegment = _ropeSegments[i];
                Vector3 velocity = thisSegment.position - thisSegment.prevPosition;
                thisSegment.prevPosition = thisSegment.position;
                thisSegment.position += velocity;
                thisSegment.position += _ropeDirection * Time.fixedDeltaTime;
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
            RopeNode firstSegment = _ropeSegments[0];
            firstSegment.position = _firerer.position;
            _ropeSegments[0] = firstSegment;

            //all other segments will be within a certain distance to it
            for (int i = 0; i < _segmentCount - 1; i++)
            {
                RopeNode segmentStart = _ropeSegments[i];
                RopeNode segmentEnd = _ropeSegments[i + 1];

                float dist = (segmentStart.position - segmentEnd.position).magnitude;
                float error = dist - _currentSegmentLength;
                Vector3 changeDir = (firstSegment.position - segmentEnd.position).normalized;
                Vector3 changeAmount = changeDir * error;

                if (i != 0)
                {
                    segmentStart.position -= changeAmount * 0.5f;
                    _ropeSegments[i] = segmentStart;
                    segmentEnd.position += changeAmount * 0.5f;
                    _ropeSegments[i + 1] = segmentEnd;
                }
                else
                {
                    segmentEnd.position += changeAmount;
                    _ropeSegments[i + 1] = segmentEnd;
                }
            }
        }
        */
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
        }

        public class RopeNode
        {
            //original source https://toqoz.fyi/game-rope.html
            public Vector3 position, prevPosition;

            public RopeNode(Vector3 pos)
            {
                position = pos;
                prevPosition = pos;
            }
        }
    }
}