using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool _hideAnimation = false;
    
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
        Destroy(gameObject);
    }
}