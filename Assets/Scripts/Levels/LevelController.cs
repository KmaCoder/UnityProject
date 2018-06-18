using System.Collections.Generic;
using System.Linq;
using CollectableObjects;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current;

        public UiLifesController LifesController;
        public UiCoinsController CoinsController;
        public UiFruitsCounter FruitsController;
        public UiDiamondsController DiamondsController;

        public Animator EndGameWindow;
        public Animator CompletedWindow;
        
        private LevelStat _levelStat;
        private readonly List<Crystal.CrystalType> _crystals = new List<Crystal.CrystalType>();
        private int _coinsCollected;
        private int _maxFruits;

        public int LivesLeft { get; private set; }

        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;
        }

        private void Start()
        {
            Resume();
            LoadSceneInfo();
        }

        private void LoadSceneInfo()
        {
            string str = PlayerPrefs.GetString(SceneManager.GetActiveScene().name, null);
            _levelStat = JsonUtility.FromJson<LevelStat>(str) ?? new LevelStat();

            LivesLeft = 3;
            _coinsCollected = 0;

            SetUpFruits();
            LifesController.SetLives(LivesLeft);
            CoinsController.SetCount(PlayerPrefs.GetInt("coins", 0));
            DiamondsController.Reset();
        }

        private void SetUpFruits()
        {
            var fruits = FindObjectsOfType<Fruit>();
            foreach (var fruit in fruits)
            {
                Debug.Log(fruit);
            }

            Debug.Log("");
            
            foreach (var fruit in _levelStat.CollectedFruits)
            {
                Debug.Log(fruit);
                fruit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
//                if (fruits.Contains(fruit))
//                {
//                    Debug.Log(fruit);
//                }
            }

            
            _maxFruits = fruits.Length;

            FruitsController.SetCount(_levelStat.CollectedFruits.Count, _maxFruits);
        }

        private void SaveSceneInfo()
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, JsonUtility.ToJson(_levelStat));
            PlayerPrefs.Save();
        }

        public void OnLevelCompleted()
        {
            _levelStat.LevelPassed = true;
            SaveSceneInfo();

            CompletedWindow.transform.Find("SettingsPanel").Find("TextFruits").GetComponent<Text>().text =
                _levelStat.CollectedFruits.Count + "/" + _maxFruits;
            CompletedWindow.transform.Find("SettingsPanel").Find("TextCoins").GetComponent<Text>().text =
                "+" + _coinsCollected;
            
            CompletedWindow.SetTrigger("open");
            Pause();
        }

        public void OnRabitDeath(HeroRabit rabit)
        {
            if (rabit.IsDead)
                return;
            rabit.Die();
            LifesController.SetLives(--LivesLeft);
            if (LivesLeft <= 0)
            {
                EndGameWindow.SetTrigger("open");
                Pause();
            }
        }

        public void OnOutOfWorld(HeroRabit rabit)
        {
            OnRabitDeath(rabit);
        }

        public void CoinCollected()
        {
            _coinsCollected += 1;
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins", 0) + 1);
            PlayerPrefs.Save();
            CoinsController.SetCount(PlayerPrefs.GetInt("coins", 0));
        }

        public void FruitCollected(Fruit fruit)
        {
            if (_levelStat.CollectedFruits.Contains(fruit))
                return;
            _levelStat.CollectedFruits.Add(fruit);
            FruitsController.SetCount(_levelStat.CollectedFruits.Count, _maxFruits);
            if (_levelStat.CollectedFruits.Count == _maxFruits)
                _levelStat.HasAllFruits = true;
        }

        public void DiamondCollected(Crystal.CrystalType type)
        {
            if (_crystals.Contains(type))
                return;
            _crystals.Add(type);
            if (_crystals.Count >= 3)
                _levelStat.HasAllCrystals = true;
            DiamondsController.DiamondCollected(type);
        }

        public void LifeCollected()
        {
            if (LivesLeft >= 3)
                return;
            LifesController.SetLives(++LivesLeft);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Resume();
        }

        public void ToMenu()
        {
            SceneManager.LoadScene("LevelChooser");
            Resume();
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}