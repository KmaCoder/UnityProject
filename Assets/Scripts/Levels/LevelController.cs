using System;
using System.Collections.Generic;
using CollectableObjects;
using Sounds;
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

        public Animator LoseGameWindow;
        public Animator CompletedWindow;

        public AudioClip AudioDeath;
        public AudioClip AudioLose;
        public AudioClip AudioWin;

        private LevelStat _levelStat;
        private readonly List<Crystal.CrystalType> _crystals = new List<Crystal.CrystalType>();
        private int _coinsCollected;
        private int _maxFruits;

        private AudioSource _soundSource;

        public int LivesLeft { get; private set; }

        private void Awake()
        {
            if (Current != null && Current != this)
            {
                Destroy(this);
                return;
            }

            Current = this;
            _soundSource = gameObject.AddComponent<AudioSource>();
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
            var globalId = 0;
            var fruits = FindObjectsOfType<Fruit>();
            Array.ForEach(fruits, fruit => fruit.Id = globalId++);

            var newList = new List<int>();

            foreach (var id in _levelStat.CollectedFruits)
            {
                var item = Array.Find(fruits, fruit => fruit.Id == id);
                if (item != null)
                {
                    newList.Add(id);
                    item.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                }
            }

            _levelStat.CollectedFruits = newList;

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

            var panel = CompletedWindow.transform.Find("SettingsPanel");
            panel.Find("TextFruits").GetComponent<Text>().text = _levelStat.CollectedFruits.Count + "/" + _maxFruits;
            panel.Find("TextCoins").GetComponent<Text>().text = "+" + _coinsCollected;
            SetUpCollectedDiamonds(panel);

            CompletedWindow.SetTrigger("open");
            Pause();

            HeroRabit.LastRabit.SetVelocityZero();
            SoundManager.Instance.PlaySound(AudioWin, _soundSource);
        }

        public void OnRabitDeath(HeroRabit rabit)
        {
            if (rabit.IsDead)
                return;
            rabit.Die();
            LifesController.SetLives(--LivesLeft);
            if (LivesLeft <= 0)
            {
                SetUpCollectedDiamonds(LoseGameWindow.transform.Find("SettingsPanel"));
                LoseGameWindow.SetTrigger("open");
                Pause();
                SoundManager.Instance.PlaySound(AudioLose, _soundSource);
            }
            else
            {
                SoundManager.Instance.PlaySound(AudioDeath, _soundSource);
            }
        }

        private void SetUpCollectedDiamonds(Transform windowPanel)
        {
            windowPanel.Find("CrystalBlue").GetComponent<Image>().sprite = _crystals.Contains(Crystal.CrystalType.Blue)
                ? DiamondsController.BlueDiamond
                : DiamondsController.Empty;
            windowPanel.Find("CrystalGreen").GetComponent<Image>().sprite =
                _crystals.Contains(Crystal.CrystalType.Green)
                    ? DiamondsController.GreenDiamond
                    : DiamondsController.Empty;
            windowPanel.Find("CrystalRed").GetComponent<Image>().sprite = _crystals.Contains(Crystal.CrystalType.Red)
                ? DiamondsController.RedDiamond
                : DiamondsController.Empty;
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

        public void FruitCollected(int fruitId)
        {
            if (_levelStat.CollectedFruits.Contains(fruitId))
                return;
            _levelStat.CollectedFruits.Add(fruitId);
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