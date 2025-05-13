using UnityEngine;
using System.Collections;

public class PatternCoroutineRunner : MonoBehaviour
{
    public void Run(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}
