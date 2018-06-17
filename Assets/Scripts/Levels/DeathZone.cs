using UnityEngine;

namespace Levels
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            HeroRabit rabit = other.GetComponent<HeroRabit>();
            if (rabit != null)
            {
                if (LevelController.Current == null)
                {
                    rabit.Die();
                    return;
                }

                LevelController.Current.OnOutOfWorld(rabit);
            }
        }
    }
}