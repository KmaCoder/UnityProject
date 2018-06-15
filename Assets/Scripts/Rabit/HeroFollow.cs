using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    private HeroRabit _rabit;

    void Start()
    {
        _rabit = HeroRabit.LastRabit;
    }

    void Update()
    {
        if (!_rabit.IsDead)
        {
            Vector3 newCameraPosition = transform.position;
            newCameraPosition.x = _rabit.transform.position.x;
            newCameraPosition.y = _rabit.transform.position.y;
            transform.position = newCameraPosition;
        }
    }
}