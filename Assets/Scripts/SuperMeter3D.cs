using UnityEngine;

public class SuperMeter3D : MonoBehaviour
{
    public GameObject[] notches; // The 10 cube notches (child objects)
    public Material notchMaterial; // Material for the notches (with _HeightCutoff)
    
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
                cutoff = 9f;
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
                cutoff = Mathf.Lerp(7f, 9f, t);
            }

            UpdateNotchHeightCutoff(i, cutoff);
        }
    }

    private void UpdateNotchHeightCutoff(int notchIndex, float targetCutoff)
    {
        if (notches[notchIndex] != null)
        {
            Material mat = notches[notchIndex].GetComponent<Renderer>().material;
            if (mat != null)
            {
                mat.SetFloat("_HeightCutoff", targetCutoff); // Update _HeightCutoff of the material
            }
        }
    }
}
