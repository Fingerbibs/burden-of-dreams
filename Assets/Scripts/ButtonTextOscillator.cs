using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextOscillator : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    private bool isSelected = false;
    public float oscillationSpeed = 2f;
    public Color colorA = Color.black;
    public Color colorB = Color.white;

    void Start()
    {
        textComponent = GetComponentInChildren<TextMeshProUGUI>();
        if (textComponent == null)
            Debug.LogError("No TextMeshProUGUI component found on button!");
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject && textComponent != null)
        {
            float t = (Mathf.Sin(Time.time * oscillationSpeed) + 1f) / 2f;
            textComponent.color = Color.Lerp(colorA, colorB, t);
        }
        else if (textComponent != null)
        {
            textComponent.color = colorA;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        if (textComponent != null)
            textComponent.color = colorA; // Reset color
    }
}
