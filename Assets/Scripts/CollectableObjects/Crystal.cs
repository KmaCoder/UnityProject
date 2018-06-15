namespace CollectableObjects
{
	public class Crystal : Collectable {

		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
		}
	}
}
