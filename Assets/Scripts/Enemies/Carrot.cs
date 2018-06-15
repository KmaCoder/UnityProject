using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float Speed;
    public float LifeTime;
    public bool Direction;

    void Start()
    {
        StartCoroutine(DestroyLater());
        GetComponent<SpriteRenderer>().flipX = Direction;
    }

    void Update()
    {
        transform.position -= new Vector3(Speed * (Direction ? 1 : -1), 0, 0);
    }

    IEnumerator DestroyLater()
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