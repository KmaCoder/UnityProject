using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UiFruitsCounter : MonoBehaviour
	{
		public int MaxFruits = 11;

		private void Start()
		{
			SetCount(0);
		}

		public void SetCount(int count)
		{
			if (count > MaxFruits)
				count = MaxFruits;
			transform.GetComponentInChildren<Text>().text = count + "/" + MaxFruits;
		}
	}
}
