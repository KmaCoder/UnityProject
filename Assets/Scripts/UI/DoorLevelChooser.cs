using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class DoorLevelChooser : MonoBehaviour
    {
        public Sprite[] NumberSprites;
        public int LoadLevelNumber = 1;
        public bool Locked;
        public bool DiamondsAllCollected;
        public bool FruitsAllCollected;

        void Start()
        {
            transform.Find("lock").GetComponent<SpriteRenderer>().enabled = Locked;
            transform.Find("diamonds").GetComponent<SpriteRenderer>().enabled = DiamondsAllCollected;
            transform.Find("fruits").GetComponent<SpriteRenderer>().enabled = FruitsAllCollected;
            transform.Find("level_num").GetComponent<SpriteRenderer>().sprite = NumberSprites[LoadLevelNumber - 1];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.GetComponent<HeroRabit>() != null && !Locked)
            {
                SceneManager.LoadScene("Level" + LoadLevelNumber);
            }
        }
    }
}