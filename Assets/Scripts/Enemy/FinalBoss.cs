using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1000f;
    private float currentHealth;

    [Header("Phase Settings")]
    public List<BossPhase> phases;
    private int currentPhaseIndex = -1;
    private BulletPattern currentBulletPattern;

    [Header("Firing")]
    public float fireCooldown = 1.5f;

    private bool isInvincible = false;
    private EnemyMovement movement;
    private Polarity polarity;

    public virtual void Initialize(Transform[] path, Vector3 offset)
    {
        movement?.Initialize(path, offset);
    }

    void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        polarity = Polarity.Light;
    }

    void Start()
    {
        AudioManager.Instance.PlayBossSpawn();
        currentHealth = maxHealth;
        CheckPhaseTransition(); // Initialize first phase
    }

    void Update()
    {
        CheckPhaseTransition();

        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            if (currentBulletPattern != null)
            {
                currentBulletPattern.Fire(transform, polarity);
            }
            fireCooldown = currentBulletPattern.fireRate;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isInvincible) return;

        AudioManager.Instance.PlayBossHit();
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void CheckPhaseTransition()
    {
        float healthPercent = currentHealth / maxHealth;

        // Iterate from highest phase down to ensure correct activation
        for (int i = phases.Count - 1; i >= 0; i--)
        {
            if (healthPercent <= phases[i].healthThreshold)
            {
                if (currentPhaseIndex != i)
                {
                    SwitchToPhase(i);
                }
                break;
            }
        }
    }

    void SwitchToPhase(int phaseIndex)
    {
        currentPhaseIndex = phaseIndex;
        StartCoroutine(PhaseTransition(phaseIndex));
    }

    public IEnumerator PhaseTransition(int phaseIndex)
    {
        isInvincible = true;       
        fireCooldown = 999f; // Prevent firing temporarily

        //PlayDissolveEffect(); // Trigger visual dissolve effect here
        AudioManager.Instance.PlayBossPhase();

        // Set phase values
        currentBulletPattern = phases[phaseIndex].bulletPattern;
        polarity = phases[phaseIndex].polarity;
        SetMinionPolarity(phaseIndex);

        yield return new WaitForSeconds(3f); // Wait before resuming fire

        fireCooldown = currentBulletPattern.fireRate;
        isInvincible = false;

        Debug.Log("Switched to phase: " + phaseIndex);
    }

    void SetMinionPolarity(int phaseIndex)
    {
        EnemyPolarity minionPolarity1 = transform.GetChild(0).GetComponent<EnemyPolarity>();
        EnemyPolarity minionPolarity2 = transform.GetChild(1).GetComponent<EnemyPolarity>();

        switch (phaseIndex)
        {
            case 0:
                minionPolarity1.polarity = Polarity.Light;
                minionPolarity2.polarity = Polarity.Light;
                //
                break;
            case 1:
                minionPolarity1.polarity = Polarity.Dark;
                minionPolarity2.polarity = Polarity.Dark;
                break;
            case 2:
                minionPolarity1.polarity = Polarity.Light;
                minionPolarity2.polarity = Polarity.Dark;
                break;
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated.");
        StartCoroutine(EndScene());
    }

    IEnumerator EndScene()
    {
        StartCoroutine(HandleMusic());

        GameObject player = GameObject.FindWithTag("Player");
        MissileDialogue victory = player.GetComponent<MissileDialogue>();
        victory.toTerminal();
        GameManager.Instance.PlayerControllable(player, false);

        yield return StartCoroutine(CameraShake(2f, 0.5f));

        Material bossMat = GetComponent<Renderer>().material;
        Renderer[] minionRenderers = GetComponentsInChildren<Renderer>().Where(r => r.gameObject != gameObject).ToArray();
        yield return StartCoroutine(BossDeathRoutine(minionRenderers, bossMat, 12f));
    }


    IEnumerator HandleMusic()
    {   
        AudioManager.Instance.StopStage();
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayBossPreDeath();
        // Wait for the sound to finish before proceeding
        yield return new WaitForSeconds(.7f);

        AudioManager.Instance.PlayBossDeath();
    }

    IEnumerator BossDeathRoutine(Renderer[] minionRenderers, Material bossMaterial, float duration)
    {
        fireCooldown = 9999f;

        for (int i = 0; i < Mathf.Min(2, transform.childCount); i++)
        {
            Transform child = transform.GetChild(i);
            ShooterEnemy shooter = child.GetComponent<ShooterEnemy>();
            shooter.enabled = false;
        }

        // Destroy all bullets right before the boss starts the death routine
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(bullet);
        }

        StartCoroutine(PanCameraUp(6f));


        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.back * 3f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = Mathf.Lerp(1f, 0f, t);
            float dissolve = Mathf.Lerp(12f, 4f, t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            // Fade minions
            foreach (Renderer rend in minionRenderers)
            {
                foreach (var mat in rend.materials)
                {
                    if (mat.HasProperty("_BaseColor"))
                    {
                        Color color = mat.color;
                        color.a = alpha;
                        mat.color = color;
                    }
                }
            }

            // Dissolve boss
            if (bossMaterial.HasProperty("_CutoffHeight"))
            {
                bossMaterial.SetFloat("_CutoffHeight", dissolve);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(LoadCreditsAfterDelay(20f));

        // Final cleanup
        foreach (Renderer rend in minionRenderers)
        {
            rend.gameObject.SetActive(false);
        }

        gameObject.SetActive(false); // Deactivate boss
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.localPosition = originalPos + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.localPosition = originalPos;
    }

    IEnumerator PanCameraUp(float duration)
    {
        Transform cam = Camera.main.transform;

        Quaternion startRot = Quaternion.Euler(90f, 0f, 0f);
        Quaternion endRot = Quaternion.Euler(0f, 0f, 0f); // Or any final orientation you want

        float elapsed = 0f;

        while (elapsed < duration)
        {
            cam.rotation = Quaternion.Slerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.rotation = endRot;

    }

    IEnumerator LoadCreditsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Credits"); // or use SceneManager.LoadScene(3); if using index
    }
} 
