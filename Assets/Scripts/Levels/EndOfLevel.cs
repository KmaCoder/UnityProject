using UnityEngine;

namespace Levels
{
    public class EndOfLevel : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HeroRabit>() != null)
                LevelController.Current.OnLevelCompleted();
        }
    }
}