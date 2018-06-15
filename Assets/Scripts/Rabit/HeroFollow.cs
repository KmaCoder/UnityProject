using UnityEngine;

namespace Rabit
{
    public class HeroFollow : MonoBehaviour
    {
        private HeroRabit _rabit;

        private void Start()
        {
            _rabit = HeroRabit.LastRabit;
        }

        private void Update()
        {
            if (_rabit.IsDead) return;
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = _rabit.transform.position.x;
            newCameraPosition.y = _rabit.transform.position.y;
            transform.position = newCameraPosition;
        }
    }
}