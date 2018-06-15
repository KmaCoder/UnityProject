using UnityEngine;

namespace UI
{
	public class UiSettingsController : MonoBehaviour {

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
