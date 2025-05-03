using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene("Quote"); // Replace with your actual game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Works only in builds
        Application.Quit();
    }
}
