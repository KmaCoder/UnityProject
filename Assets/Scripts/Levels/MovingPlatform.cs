using UnityEngine;

namespace Levels
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 MoveBy;
        public float Speed = 0.5f;
        public float Timeout = 1f;

        private Vector3 _pointA;
        private Vector3 _pointB;
        private bool _goingToA;

        private float _timeToWait;

        // Use this for initialization
        private void Start()
        {
            _pointA = transform.position;
            _pointB = transform.position + MoveBy;
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 myPos = transform.position;
            Vector3 target = _goingToA ? _pointA : _pointB;

            if (IsArrived(myPos, target))
            {
                _timeToWait = Timeout;
                _goingToA = !_goingToA;
            }
            else
            {
                if (_timeToWait > 0)
                {
                    _timeToWait -= Time.deltaTime;
                }
                else
                {
                    myPos = Vector3.MoveTowards(myPos, target, Speed);
                }
            }

            transform.position = myPos;
        }

        private bool IsArrived(Vector3 pos, Vector3 target)
        {
            pos.z = 0;
            target.z = 0;
            return Vector3.Distance(pos, target) < 0.02f;
        }
    }
}