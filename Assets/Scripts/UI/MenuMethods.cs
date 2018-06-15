using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuMethods : MonoBehaviour
    {    
        public void OnPlayClicked()
        {
            SceneManager.LoadScene("LevelChooser");
        }
    }
}