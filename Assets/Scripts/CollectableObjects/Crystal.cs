using Levels;

namespace CollectableObjects
{
	public class Crystal : Collectable
	{
		public CrystalType Type = CrystalType.Blue;
		
		public enum CrystalType
		{
			Blue, Green, Red
		}

		protected override void OnRabitHit(HeroRabit rabit)
		{
			CollectedHide();
			LevelController.Current.DiamondCollected(Type);
		}
	}
}
