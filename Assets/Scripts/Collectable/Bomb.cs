public class Bomb : Collectable {

	protected override void OnRabitHit(HeroRabit rabit)
	{
		if (rabit.InvulnerableTimeLeft <= 0)
		{
			rabit.HitRabbit();
			CollectedHide();
		}
	}
}
