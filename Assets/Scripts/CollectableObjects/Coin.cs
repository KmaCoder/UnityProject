﻿namespace CollectableObjects
{
    public class Coin : Collectable
    {
        protected override void OnRabitHit(HeroRabit rabit)
        {
            CollectedHide();
        }
    }
}