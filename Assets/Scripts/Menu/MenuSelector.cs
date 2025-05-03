using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject firstSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
