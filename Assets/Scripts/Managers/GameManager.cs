using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Info")]
    public int playerLives = 3;

    [Header("UI")]
    public LivesUI livesUI;
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

    void Start()
    {
        AudioManager.Instance?.PlayStage1Theme();
        // Initialize the lives UI counter
        livesUI.UpdateLives(playerLives);
    }

    public void PlayerDied(GameObject player)
    {
        playerLives--;
        Debug.Log("Lives left: " + playerLives);

        if (livesUI != null)
            livesUI.UpdateLives(playerLives);

        if (playerLives <= 0)
            GameOver();
        else
            RespawnPlayer(player);
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