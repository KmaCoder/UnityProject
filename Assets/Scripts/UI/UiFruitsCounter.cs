using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UiFruitsCounter : MonoBehaviour
	{
		public void SetCount(int count, int max)
		{
			transform.GetComponentInChildren<Text>().text = count + "/" + max;
		}
	}
}
