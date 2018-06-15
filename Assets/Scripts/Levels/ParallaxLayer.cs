using UnityEngine;

namespace Levels
{
    public class ParallaxLayer : MonoBehaviour
    {
        public float Slowdown = 0.5f;
        private Vector3 _lastPosition;

        private void Awake()
        {
            _lastPosition = Camera.main.transform.position;
        }

        private void LateUpdate()
        {
            Vector3 newPosition = Camera.main.transform.position;
            Vector3 diff = newPosition - _lastPosition;
            _lastPosition = newPosition;
            Vector3 myPos = transform.position;
            myPos += Slowdown * diff;
            transform.position = myPos;
        }
    }
}