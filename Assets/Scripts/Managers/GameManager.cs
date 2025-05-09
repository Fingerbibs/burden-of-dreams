using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Player Info")]
    public int playerLives = 3;
    private bool isInvincible = false;
    public float startupTime = 15;

    public float invincibilityDuration = 20;
    // Disintegrate Parameters
    private float fromHeight = 8f;
    private float toHeight = 5f;
    private float duration = 1f;
    private Material material;
    private Material shieldMaterial;
    private Color faded;

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
        GameObject player = GameObject.Find("Player");
        StartCoroutine(StartControl(player));
    }

    public void PlayerDied(GameObject player)
    {
        if(!isInvincible)
        {
            AudioManager.Instance.PlayPlayerDeath();
            playerLives--;
            Debug.Log("Lives left: " + playerLives);

            if (livesUI != null)
                livesUI.UpdateLives(playerLives);

            if (playerLives <= 0)
                GameOver();
            else
                RespawnPlayer(player);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        StartCoroutine(RespawnFullSequence(player));
    }

    private IEnumerator RespawnFullSequence(GameObject player)
    {
        yield return StartCoroutine(DeathSequence(player));
        // Move player after dissolving out
        player.transform.position = new Vector3(0f, 6f, -5f);
        yield return StartCoroutine(RespawnSequence(player));
    }

    private IEnumerator DeathSequence(GameObject player)
    {
        // Set Invincible 
        isInvincible = true;

        // Disable movement & shooting
        PlayerControllable(player, false);

        // Get Player & Shield Materials
        material = player.GetComponent<Renderer>().material;
        shieldMaterial = player.transform.Find("Shield")?.GetComponent<Renderer>().material;
        Color shieldStartColor = shieldMaterial.color;

        // Play death sound
        AudioManager.Instance.PlayPlayerDeath();

        // Dissintegrate
        float t = 0f;
        while (t < duration)
        {
            float h = Mathf.Lerp(fromHeight, toHeight, t / duration);
            material.SetFloat("_CutoffHeight", h);

            faded = shieldStartColor;
            faded.a = Mathf.Lerp(1f, 0f, t / duration);
            shieldMaterial.color = faded;

            t += Time.deltaTime;
            yield return null;
        }
        material.SetFloat("_CutoffHeight", toHeight);

        faded = shieldStartColor;
        faded.a = 0f;
        shieldMaterial.color = faded;
    }

    private IEnumerator RespawnSequence(GameObject player)
    {
        // Materialize
        yield return StartCoroutine(Materialize(player));

        yield return new WaitForSeconds(0.5f); // Wait before allowing movement again

        PlayerControllable(player, true);
        // Continue invincibility after respawn delay
        yield return new WaitForSeconds(invincibilityDuration - 2f);
        isInvincible = false;
    }

    private IEnumerator Materialize(GameObject player)
    {
        // Grab Player & Shield Materials
        material = player.GetComponent<Renderer>().material;
        shieldMaterial = player.transform.Find("Shield")?.GetComponent<Renderer>().material;

        // Start Shield Alpha at 0
        Color shieldBaseColor = shieldMaterial != null ? shieldMaterial.color : Color.clear;
        shieldBaseColor.a = 0f; // start from invisible
        shieldMaterial.color = shieldBaseColor;

        // Play Materialize Sound
        AudioManager.Instance.PlayAbsorb();

        // Materialize Player & Shield
        float t = 0f;
        while (t < duration)
        {
            float h = Mathf.Lerp(toHeight, fromHeight, t / duration);
            material.SetFloat("_CutoffHeight", h);

            faded = shieldBaseColor;
            faded.a = Mathf.Lerp(0f, 1f, t / duration);
            shieldMaterial.color = faded;

            t += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_CutoffHeight", fromHeight);

        faded = shieldBaseColor;
        faded.a = 1f;
        shieldMaterial.color = faded;
    }

    private void PlayerControllable(GameObject player, bool controllable){
        var movement = player.GetComponent<PlayerMovement>();
        var shooting = player.GetComponent<PlayerShooting>();
        var polarity = player.GetComponent<PlayerPolarityController>();

        movement.enabled = controllable;
        shooting.enabled = controllable;
        polarity.enabled = controllable;
    }

    private IEnumerator StartControl(GameObject player)
    {
        PlayerControllable(player, false);
        yield return new WaitForSeconds(startupTime);
        PlayerControllable(player, true);
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(gameOverScene);
    }
}