using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.PlayDeath3();
    }
    
    public void PlayGame()
    {
        // Play sound first
        AudioManager.Instance.PlayMenuSelect();
        
        // Start a coroutine to delay the scene load
        StartCoroutine(LoadGameSceneWithDelay());
    }

    private IEnumerator LoadGameSceneWithDelay()
    {
        // Wait for the duration of the sound (you can adjust the time as needed)
        yield return new WaitForSeconds(1f); // Adjust the delay to match your sound duration
        
        // Load the game scene
        SceneManager.LoadScene("Main"); // Replace with your actual game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Works only in builds
        Application.Quit();
    }
}
