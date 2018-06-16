using Levels;

namespace CollectableObjects
{
	public class Crystal : Collectable
	{
		public DiamondType Type = DiamondType.Blue;
		
		public enum DiamondType
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
