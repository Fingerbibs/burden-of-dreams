using UnityEngine;

public class PlayQuote : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlayQuoteTheme();
    }
}
