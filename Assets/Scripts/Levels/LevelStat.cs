using System.Collections.Generic;
using CollectableObjects;

namespace Levels
{
	[System.Serializable]
	public class LevelStat {
		public bool LevelPassed = false;
		public bool HasAllCrystals = false;
		public List<int> CollectedFruits = new List<int>();
		public bool HasAllFruits = false;
	}
}