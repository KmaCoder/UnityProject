using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UILifesController : MonoBehaviour
    {
        public Sprite Heart;
        public Sprite HeartBG;

        public int LifesCount
        {
            get { return _lifesCount; }
            set
            {
                if (value > transform.childCount)
                    value = transform.childCount;
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).GetComponent<Image>().sprite = value - 1 >= i ? Heart : HeartBG;
            }
        }

        private int _lifesCount = 0;

        void Start()
        {
            LifesCount = 0;
        }
    }
}