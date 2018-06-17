using Levels;
using UnityEngine;

namespace CollectableObjects
{
	public class Fruit : Collectable
	{
//		[HideInInspector]
		public int Id;

		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
			LevelController.Current.FruitCollected(Id);
		}
	}
}
