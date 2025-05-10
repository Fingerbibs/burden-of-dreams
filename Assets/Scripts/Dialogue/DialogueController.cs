using UnityEngine;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public Dialogue dialogue; // Reference to the dialogue ScriptableObject
    public DialogueManager dialogueManager; // Reference to the DialogueManager

    void Start()
    {
        // StartCoroutine(BeginDialogueAfterDelay(0f));
        dialogueManager.StartDialogue(dialogue);
    }

    
}
