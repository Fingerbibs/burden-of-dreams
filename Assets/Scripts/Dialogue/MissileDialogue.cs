using UnityEngine;

public class MissileDialogue : MonoBehaviour
{
    public Dialogue dialogue; // Reference to the dialogue ScriptableObject
    public DialogueManager dialogueManager; // Reference to the DialogueManager

    public void toTerminal(){
        dialogueManager.StartDialogue(dialogue);
    }
}
