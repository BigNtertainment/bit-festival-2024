using Dialogues;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(DialogueLine dialogueLine)
    {
        if (FindFirstObjectByType<DialogueManager>() is not { } dialogueManager)
        {
            Debug.LogWarning("No DialogueManager found.");
            return;
        }

        dialogueManager.StartDialogue(dialogueLine);
    }
}