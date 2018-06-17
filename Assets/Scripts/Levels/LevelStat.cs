using System.Collections.Generic;

[System.Serializable]
public class LevelStat {
	public bool levelPassed = false;
	public bool hasAllCrystals = false;
	public List<int> collectedFruits = new List<int>();
	public bool hasAllFruits = false;
}