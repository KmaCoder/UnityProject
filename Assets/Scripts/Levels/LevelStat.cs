using System.Collections.Generic;
using CollectableObjects;

namespace Levels
{
	[System.Serializable]
	public class LevelStat {
		public bool LevelPassed = false;
		public bool HasAllCrystals = false;
		public List<Fruit> CollectedFruits = new List<Fruit>();
		public bool HasAllFruits = false;
	}
}