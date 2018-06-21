using Levels;

namespace CollectableObjects
{
    public class Life : Collectable
    {
        protected override void OnRabitHit(HeroRabit rabit)
        {
            LevelController.Current.LifeCollected();
            CollectedHide();
        }
    }
}