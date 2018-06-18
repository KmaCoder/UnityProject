using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiLifesController : MonoBehaviour
    {
        public Sprite Heart;
        public Sprite HeartEmpty;

        public void SetLives(int value)
        {
            if (value > transform.childCount)
                value = transform.childCount;
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).GetComponent<Image>().sprite = value - 1 >= i ? Heart : HeartEmpty;
        }
    }
}