namespace CollectableObjects
{
	public class Mushroom : Collectable {

		protected override void OnRabitHit(HeroRabit rabit)
		{
			rabit.MakeBigger();
			CollectedHide();
		}
	}
}
