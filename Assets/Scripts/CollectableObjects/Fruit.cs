using Levels;
using UnityEngine;

namespace CollectableObjects
{
	[System.Serializable]
	public class Fruit : Collectable
	{
		public int Id { get; set; }
		
		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
			LevelController.Current.FruitCollected(Id);
		}
	}
}
