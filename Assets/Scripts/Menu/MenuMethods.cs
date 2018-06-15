using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMethods : MonoBehaviour
{    
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Level1");
    }
}