using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerPolarityController : MonoBehaviour
{
    public Polarity currentPolarity = Polarity.Light;

    [Header("Animation Duration")]
    public float rotationDuration = 0.2f; // How fast player rotates
    public float colorTransitionDuration = 0.2f; // How fast color shifts

    //Polarity Change Event
    public delegate void PolarityChanged(Polarity newPolarity);
    public event PolarityChanged OnPolarityChanged;

    private Material playerMaterial;
    private Color lightColor = Color.white;
    private Color darkColor = Color.black;
    private bool isRotating = false;

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || (Gamepad.current != null && Gamepad.current.rightShoulder.isPressed))
        {
            SwitchPolarity();
        }
    }

    void SwitchPolarity()
    {
        if (!isRotating)
        {
            // Switch polarity
            currentPolarity = currentPolarity == Polarity.Light ? Polarity.Dark : Polarity.Light;

            // Flip player around Y-axis
            float targetY = currentPolarity == Polarity.Light ? 90f : 270f;
            StartCoroutine(RotateToY(targetY));

            // Change color
            Color targetColor = currentPolarity == Polarity.Light ? lightColor : darkColor;
            StartCoroutine(LerpColor(targetColor));

            // Broadcast polarity change
            OnPolarityChanged?.Invoke(currentPolarity);
        }
    }

    IEnumerator RotateToY(float targetY)
    {
        // Gradcually rotate player ship
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0f, targetY, 0f);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / rotationDuration;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        isRotating = false;
    }

    IEnumerator LerpColor(Color targetColor)
    {
        Color startColor = playerMaterial.color;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / colorTransitionDuration;
            playerMaterial.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        playerMaterial.color = targetColor;  // Ensure exact final value
    }
}
