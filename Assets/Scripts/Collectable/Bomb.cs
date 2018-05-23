using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	protected override void OnRabitHit(HeroRabit rabit)
	{
		if (rabit.InvulnerableTimeLeft > 0)
			return;
		if(rabit.IsScaled)
			rabit.MakeSmaller();
		else 
			LevelController.Current.OnRabitDeath(rabit);
		CollectedHide();
	}
}
