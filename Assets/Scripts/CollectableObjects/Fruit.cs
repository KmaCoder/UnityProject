using Levels;

namespace CollectableObjects
{
	public class Fruit : Collectable {

		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
			LevelController.Current.FruitCollected();
		}
	}
}
