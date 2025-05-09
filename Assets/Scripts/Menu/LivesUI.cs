using UnityEngine;
using TMPro;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesText;

    public void UpdateLives(int lives)
    {
        livesText.text = $"Lives = {lives};";
    }
}
