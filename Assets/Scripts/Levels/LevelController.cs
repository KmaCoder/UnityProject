using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current;

        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;
        }

        public void OnRabitDeath(HeroRabit rabit)
        {
            rabit.Die();
        }

        public void OnOutOfWorld(HeroRabit rabit)
        {
            rabit.Die();
            rabit.Revive();
        }
    }
}