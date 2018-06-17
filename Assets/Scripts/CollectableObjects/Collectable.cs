using UnityEngine;

namespace CollectableObjects
{
    public class Collectable : MonoBehaviour
    {
        private bool _hideAnimation;

        protected virtual void OnRabitHit(HeroRabit rabit)
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!_hideAnimation)
            {
                HeroRabit rabit = other.GetComponent<HeroRabit>();
                if (rabit != null)
                {
                    OnRabitHit(rabit);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEnter2D(other);
        }

        public void CollectedHide()
        {
            _hideAnimation = true;
            GetComponent<Animator>().SetTrigger("hide");
        }

        public void OnAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}