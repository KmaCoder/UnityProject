using CollectableObjects;
using UI;
using UnityEngine;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current;

        public UiLifesController LifesController;
        public UiCoinsController CoinsController;
        public UiFruitsCounter FruitsController;
        public UiDiamondsController DiamondsController;

        private int _lifesLeft;
        private int _coinsCollected;
        private int _fruitsCollected;
        private int _diamondsCollected;

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
            Reset();
        }

        private void Reset()
        {
            _lifesLeft = 3;
            _coinsCollected = 0;
            _fruitsCollected = 0;
            _diamondsCollected = 0;
            LifesController.SetLifes(_lifesLeft);
            CoinsController.SetCount(_coinsCollected);
            FruitsController.SetCount(_fruitsCollected);
            DiamondsController.Reset();
        }

        public void OnRabitDeath(HeroRabit rabit)
        {
            if (rabit.IsDead)
                return;
            rabit.Die();
            _lifesLeft--;
            LifesController.SetLifes(_lifesLeft);
            if (_lifesLeft <= 0)
                Debug.Log("END OF GAME");
        }

        public void OnOutOfWorld(HeroRabit rabit)
        {
            OnRabitDeath(rabit);
        }

        public void CoinCollected()
        {
            CoinsController.SetCount(++_coinsCollected);
        }

        public void FruitCollected()
        {
            FruitsController.SetCount(++_fruitsCollected);
        }

        public void DiamondCollected(Crystal.DiamondType type)
        {
            ++_diamondsCollected;
            DiamondsController.DiamondCollected(type);
        }
    }
}