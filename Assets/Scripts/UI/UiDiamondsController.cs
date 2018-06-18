using System;
using CollectableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiDiamondsController : MonoBehaviour
    {
        public Sprite BlueDiamond;
        public Sprite GreenDiamond;
        public Sprite RedDiamond;
        public Sprite Empty;

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < transform.childCount; i++)
                SetDiamond(transform.GetChild(i).name, Empty);
        }

        public void DiamondCollected(Crystal.CrystalType type)
        {
            switch (type)
            {
                case Crystal.CrystalType.Blue:
                    SetDiamond("blue_diamond", BlueDiamond);
                    break;
                case Crystal.CrystalType.Green:
                    SetDiamond("green_diamond", GreenDiamond);
                    break;
                case Crystal.CrystalType.Red:
                    SetDiamond("red_diamond", RedDiamond);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        private void SetDiamond(string dName, Sprite sprite)
        {
            try
            {
                transform.Find(dName).GetComponent<Image>().sprite = sprite;
            }
            catch (NullReferenceException)
            {
                Debug.LogError("No sprite with name " + dName);
            }
        }
    }
}