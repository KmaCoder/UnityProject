using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuMethods : MonoBehaviour
    {    
        public void OnPlayClicked()
        {
            SceneManager.LoadScene("LevelChooser");
        }
    }
}