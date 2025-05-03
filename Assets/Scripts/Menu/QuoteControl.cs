using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class QuoteControl : MonoBehaviour
{
    public TMP_Text[] quoteTexts;  // Array to hold multiple TMP_Text components
    public GameObject[] objectsToFade;
    public float fadeDuration = 2f;  // Duration of the fade-in effect
    public float delayBeforeSceneLoad = 2f;


    void Start()
    {
        // Start the fade-in effect for all text fields at the same time
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float timeElapsed = 0f;

        // Initialize all text fields to be fully transparent
        foreach (var quoteText in quoteTexts)
        {
            Color startColor = quoteText.color;
            startColor.a = 0f;  // Set alpha to 0 for transparent
            quoteText.color = startColor;  // Apply the transparent color to the text
        }

        // Initialize all objects to be fully transparent
        foreach (var obj in objectsToFade)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                Color startColor = objRenderer.material.color;
                startColor.a = 0f;  // Set alpha to 0 for transparent
                objRenderer.material.color = startColor;  // Apply the transparent color to the object
            }
        }

        // Gradually change the alpha value of all text fields and objects' colors to create the fade-in effect
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration); // Interpolate alpha from 0 to 1

            // Apply the fading effect to each text field
            foreach (var quoteText in quoteTexts)
            {
                quoteText.color = new Color(quoteText.color.r, quoteText.color.g, quoteText.color.b, alpha);
            }

            // Apply the fading effect to each object
            foreach (var obj in objectsToFade)
            {
                Renderer objRenderer = obj.GetComponent<Renderer>();
                if (objRenderer != null)
                {
                    objRenderer.material.color = new Color(objRenderer.material.color.r, objRenderer.material.color.g, objRenderer.material.color.b, alpha);
                }
            }

            yield return null;
        }

        // Ensure all text fields and objects are fully opaque at the end
        foreach (var quoteText in quoteTexts)
        {
            quoteText.color = new Color(quoteText.color.r, quoteText.color.g, quoteText.color.b, 1f);
        }

        foreach (var obj in objectsToFade)
        {
            Renderer objRenderer = obj.GetComponent<Renderer>();
            if (objRenderer != null)
            {
                objRenderer.material.color = new Color(objRenderer.material.color.r, objRenderer.material.color.g, objRenderer.material.color.b, 1f);
            }
        }

        // Wait for a short delay before loading the next scene
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        // Load the next scene after the delay
        SceneManager.LoadScene("Main");
    }
}