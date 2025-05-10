using UnityEngine;

[CreateAssetMenu(fileName = "LevelBounds", menuName = "Game/Level Bounds")]
public class LevelBounds : ScriptableObject
{
    public float xMin, xMax;
    public float yMin, yMax;
    public float zMin, zMax;  // if you need depth
}
