using UnityEngine;

public class SuperMeter3D : MonoBehaviour
{
    public GameObject[] notches; // The 10 cube notches (child objects)
    public Material notchMaterial; // Material for the notches (with _HeightCutoff)

    private Vector3[] rotationAxes;   // Randomized rotation directions per notch
    private float rotationSpeed = 50f;

    void Start()
    {
        ClearMeter();

        // Generate random axes for each notch
        rotationAxes = new Vector3[notches.Length];
        for (int i = 0; i < notches.Length; i++)
        {
            rotationAxes[i] = Random.onUnitSphere; // random direction
        }
    }

    void Update()
    {
        for (int i = 0; i < notches.Length; i++)
        {
            if (notches[i] != null)
            {
                notches[i].transform.Rotate(rotationAxes[i] * rotationSpeed * Time.deltaTime);
            }
        }
    }
    
    public void UpdateMeter(float totalSuperMeterValue)
    {
        for (int i = 0; i < notches.Length; i++)
        {
            float segmentStart = i * 10f;
            float segmentEnd = (i + 1) * 10f;

            float cutoff;

            if (totalSuperMeterValue >= segmentEnd)
            {
                // This notch is fully filled
                cutoff = 10f;
            }
            else if (totalSuperMeterValue <= segmentStart)
            {
                // This notch is empty
                cutoff = 7f;
            }
            else
            {
                // This notch is partially filled
                float t = (totalSuperMeterValue - segmentStart) / 10f;
                cutoff = Mathf.Lerp(7f, 10f, t);
            }

            UpdateNotchHeightCutoff(i, cutoff);
        }
    }

    public void ClearMeter()
    {
        for (int i = 0; i < notches.Length; i++)
        {
            UpdateNotchHeightCutoff(i, 7f); // 7f represents empty
        }
    }

    private void UpdateNotchHeightCutoff(int notchIndex, float targetCutoff)
    {
        if (notches[notchIndex] != null)
        {
            Material mat = notches[notchIndex].GetComponent<Renderer>().material;
            if (mat != null)
            {
                mat.SetFloat("_CutoffHeight", targetCutoff); // Update _HeightCutoff of the material
            }
        }
    }
}
