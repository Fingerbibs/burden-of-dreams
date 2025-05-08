using UnityEngine;
using UnityEngine.Playables;

public class SkipToTime : MonoBehaviour
{
    public PlayableDirector playableDirector;  // Reference to the PlayableDirector
    public float skipToTime = 20f;  // Time to skip to (in seconds)

    void Start()
    {
        // Make sure we start playing from the beginning
        playableDirector.Play();
    }

    public void SkipToEvent()
    {
        // Skip to a specific time in the timeline (e.g., 20 seconds)
        playableDirector.time = skipToTime;
        playableDirector.Evaluate();  // Refresh the timeline at the new time
    }

    // You can call this method via a UI button, or other events
}