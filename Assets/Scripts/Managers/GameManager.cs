using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerLives = 3;
    public string gameOverScene = "GameOver";

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes if needed
    }

    public void PlayerDied(GameObject player)
    {
        playerLives--;
        Debug.Log("Lives left: " + playerLives);

        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            RespawnPlayer(player);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // Reset player position and state
        // Ideally you cache or tag the spawn point
        player.transform.position = new Vector3(0f, 6f, -5f);

        // Optionally disable/re-enable movement or visuals here
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(gameOverScene);
    }
}