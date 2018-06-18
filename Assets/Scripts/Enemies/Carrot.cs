using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class Carrot : MonoBehaviour
    {
        public float Speed;
        public float LifeTime;
        public bool Direction;

        private void Start()
        {
            StartCoroutine(DestroyLater());
            GetComponent<SpriteRenderer>().flipX = Direction;
        }

        private void FixedUpdate()
        {
            transform.position -= new Vector3(Speed * (Direction ? 1 : -1), 0, 0);
        }

        private IEnumerator DestroyLater()
        {
            yield return new WaitForSeconds(LifeTime);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<HeroRabit>() != null)
            {
                other.GetComponent<HeroRabit>().HitRabbit();
                Destroy(gameObject);
            }
        }
    }
}