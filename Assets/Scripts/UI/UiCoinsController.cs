using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UiCoinsController : MonoBehaviour {

		private void Start()
		{
			SetCount(0);
		}

		public void SetCount(int count)
		{
			transform.GetComponentInChildren<Text>().text = count.ToString();
		}
	}
}
