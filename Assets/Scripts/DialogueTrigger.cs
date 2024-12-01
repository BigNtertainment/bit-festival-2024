using Dialogues;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private DialogueManager _dialogueManager;

    private void Start()
    {
        UpdateDialogueManager();
    }

    private void UpdateDialogueManager()
    {
        if (_dialogueManager is not null)
        {
            return;
        }

        _dialogueManager = FindFirstObjectByType<DialogueManager>();

        if (_dialogueManager is null)
        {
            Debug.LogWarning("No DialogueManager found.");
        }
    }

    public void TriggerDialogue(DialogueLine dialogueLine)
    {
        UpdateDialogueManager();
        _dialogueManager?.StartDialogue(dialogueLine);
    }

    public bool IsDialogueRunning()
    {
        UpdateDialogueManager();
        return _dialogueManager?.IsDialogueRunning() ?? false;
    }
}