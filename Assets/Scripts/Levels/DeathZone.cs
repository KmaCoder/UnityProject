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
                LevelController.Current.OnOutOfWorld(rabit);
            }
        }
    }
}