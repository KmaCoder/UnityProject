using Levels;
using UnityEngine;

namespace CollectableObjects
{
	[System.Serializable]
	public class Fruit : Collectable
	{
		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
			LevelController.Current.FruitCollected(this);
		}
	}
}
