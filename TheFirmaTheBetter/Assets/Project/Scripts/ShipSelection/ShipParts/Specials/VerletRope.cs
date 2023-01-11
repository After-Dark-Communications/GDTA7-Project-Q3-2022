using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipParts.Specials
{
    public class VerletRope : MonoBehaviour
    {
        [SerializeField]
        private Transform EndAttachedTransform;

        private LineRenderer _lineRenderer;
        private List<RopeSegment> _ropeSegments = new List<RopeSegment>();
        [SerializeField]
        private float _segmentLength = 0.25f;
        private const int _segmentCount = 35;
        private const float _lineWidth = 0.1f;
        private const float _rotateTime = 10f;
        private const int _constraintSimulations = 50;//higher should get better results
        [SerializeField]
        private Vector3 _gravityForce = Physics.gravity;//TODO: set this to be the direction of the hooking ship
        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            Vector3 ropeStart = transform.position;

            for (int i = 0; i < _segmentCount; i++)
            {
                _ropeSegments.Add(new RopeSegment(ropeStart));
                ropeStart.x -= _segmentLength;
            }
            _lineRenderer.startWidth = _lineWidth;
            _lineRenderer.endWidth = _lineWidth;
        }

        // Update is called once per frame
        void Update()
        {
            DrawRope();
        }
        private void LateUpdate()
        {
            //lerp rotation of attached object
            EndAttachedTransform.rotation = Quaternion.Lerp(EndAttachedTransform.rotation, Quaternion.LookRotation(_ropeSegments[_segmentCount - 2].CurrentPos), _rotateTime * Time.deltaTime);
            //adjust position of attached object
            EndAttachedTransform.position = _ropeSegments[_segmentCount - 1].CurrentPos;
        }
        private void FixedUpdate()
        {
            Simulate();
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
                thisSegment.CurrentPos += _gravityForce * Time.fixedDeltaTime;
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
            firstSegment.CurrentPos = transform.position;
            _ropeSegments[0] = firstSegment;

            //all other segments will be within a certain distance to it
            for (int i = 0; i < _segmentCount - 1; i++)
            {
                RopeSegment segmentStart = _ropeSegments[i];
                RopeSegment segmentEnd = _ropeSegments[i + 1];

                float dist = (segmentStart.CurrentPos - segmentEnd.CurrentPos).magnitude;
                float error = dist - _segmentLength;
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