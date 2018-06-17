using CollectableObjects;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private int _lifesLeft;
        private int _diamondsCollected;
        private LevelStat _levelStat;

        public int LifesLeft
        {
            get { return _lifesLeft; }
        }

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
            LoadSceneInfo();
        }

        private void LoadSceneInfo()
        {
            string str = PlayerPrefs.GetString(SceneManager.GetActiveScene().name, null);
            _levelStat = JsonUtility.FromJson<LevelStat>(str) ?? new LevelStat();

            _lifesLeft = 3;
            _diamondsCollected = 0;

            SetUpFruits();
            LifesController.SetLifes(_lifesLeft);
            CoinsController.SetCount(PlayerPrefs.GetInt("coins", 0));
            DiamondsController.Reset();
        }

        private void SetUpFruits()
        {
            Fruit[] fruits = FindObjectsOfType<Fruit>();
            for (int i = 0; i < fruits.Length; i++)
            {
                fruits[i].Id = i;
            }

            foreach (int id in _levelStat.collectedFruits)
            {
                if (fruits[id] == null)
                    _levelStat.collectedFruits.Remove(id);
                fruits[id].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }
            
            FruitsController.SetCount(_levelStat.collectedFruits.Count);
            FruitsController.MaxFruits = fruits.Length;
        }

        private void SaveSceneInfo()
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, JsonUtility.ToJson(_levelStat));
        }

        public void OnLevelCompleted()
        {
            SaveSceneInfo();
            CompletedWindow.SetTrigger("open");
        }

        public void OnRabitDeath(HeroRabit rabit)
        {
            if (rabit.IsDead)
                return;
            rabit.Die();
            _lifesLeft--;
            LifesController.SetLifes(_lifesLeft);
            if (_lifesLeft <= 0)
            {
                EndGameWindow.SetTrigger("open");
            }
        }

        public void OnOutOfWorld(HeroRabit rabit)
        {
            OnRabitDeath(rabit);
        }

        public void CoinCollected()
        {
            int coins = PlayerPrefs.GetInt("coins", 0) + 1;
            PlayerPrefs.SetInt("coins", coins);
            CoinsController.SetCount(coins);
        }

        public void FruitCollected(int id)
        {
            if (_levelStat.collectedFruits.IndexOf(id) >= 0)
                return;
            _levelStat.collectedFruits.Add(id);
            FruitsController.SetCount(_levelStat.collectedFruits.Count);
            if (_levelStat.collectedFruits.Count == FruitsController.MaxFruits)
                _levelStat.hasAllFruits = true;
        }

        public void DiamondCollected(Crystal.DiamondType type)
        {
            ++_diamondsCollected;
            if (_diamondsCollected >= 3)
                _levelStat.hasAllCrystals = true;
            DiamondsController.DiamondCollected(type);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ToMenu()
        {
            SceneManager.LoadScene("LevelChooser");
        }
    }
}