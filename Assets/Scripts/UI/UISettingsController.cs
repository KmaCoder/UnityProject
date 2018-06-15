using UnityEngine;

namespace Menu
{
	public class UISettingsController : MonoBehaviour {

		public void OpenSettings()
		{
			GetComponent<Animator>().SetTrigger("open");
		}
    
		public void CloseSettings()
		{
			GetComponent<Animator>().SetTrigger("close");
		}
	}
}
