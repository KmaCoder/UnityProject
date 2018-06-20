using Levels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class DoorLevelChooser : MonoBehaviour
    {
        public Sprite[] NumberSprites;
        public int LoadLevelNumber = 1;
        private bool _locked;

        void Start()
        {
            if (LoadLevelNumber > 1)
            {
                var prevLvlStr = PlayerPrefs.GetString("Level" + (LoadLevelNumber - 1), null);
                var prevLevelStat = JsonUtility.FromJson<LevelStat>(prevLvlStr) ?? new LevelStat();
                _locked = !prevLevelStat.LevelPassed;
            }
            else
            {
                _locked = false;
            }

            var str = PlayerPrefs.GetString("Level" + LoadLevelNumber, null);
            var levelStat = JsonUtility.FromJson<LevelStat>(str) ?? new LevelStat();

            transform.Find("lock").GetComponent<SpriteRenderer>().enabled = _locked;
            transform.Find("diamonds").GetComponent<SpriteRenderer>().enabled = levelStat.HasAllCrystals;
            transform.Find("fruits").GetComponent<SpriteRenderer>().enabled = levelStat.HasAllFruits;
            transform.Find("passed").GetComponent<SpriteRenderer>().enabled = levelStat.LevelPassed;
            transform.Find("level_num").GetComponent<SpriteRenderer>().sprite = NumberSprites[LoadLevelNumber - 1];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.GetComponent<HeroRabit>() != null && !_locked)
            {
                SceneManager.LoadScene("Level" + LoadLevelNumber);
            }
        }
    }
}