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

    private GameObject firePoint1;
    private GameObject firePoint2;

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;

        firePoint1 = transform.GetChild(1).gameObject;
        firePoint2 = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || (Gamepad.current != null && Gamepad.current.rightShoulder.isPressed))
        {
            AudioManager.Instance.PlayPlayerShift();
            SwitchPolarity();
        }
    }

    void SwitchPolarity()
    {
        if (!isRotating)
        {
            // Switch polarity
            currentPolarity = currentPolarity == Polarity.Light ? Polarity.Dark : Polarity.Light;

            // Change fire points
            float targetX = currentPolarity == Polarity.Light ? -1f : 1f;

            Vector3 lp1 = firePoint1.transform.localPosition;
            lp1.x = targetX;
            firePoint1.transform.localPosition = lp1;

            Vector3 lp2 = firePoint2.transform.localPosition;
            lp2.x = targetX;
            firePoint2.transform.localPosition = lp2;
            
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
        Color startColor = playerMaterial.GetColor("_Albedo"); // Albedo color
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / colorTransitionDuration;
            Color currentColor = Color.Lerp(startColor, targetColor, t);
            playerMaterial.SetColor("_Albedo", currentColor);
            yield return null;
        }

        playerMaterial.SetColor("_Albedo", targetColor); // Finalize color
    }
}
