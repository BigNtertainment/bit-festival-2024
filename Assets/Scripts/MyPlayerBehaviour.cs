using Dialogues;
using UnityEngine;

public class MyPlayerBehaviour : MonoBehaviour
{
    private readonly DialogueLine _dialogueLine = new DialogueLine
    {
        CharacterName = "Executioner",
        Text = "Arrggh. I'm gona execute him now.",
        Answers = new DialogueAnswer[]
        {
            new()
            {
                Text = "[Continue]",
                NextDialogueLine = new DialogueLine
                {
                    CharacterName = "Executioner",
                    Text = "Oh no! What happened to the platform!?",
                    Answers = new DialogueAnswer[]
                    {
                        new()
                        {
                            Text = "[Under your breath] There's hope",
                        }
                    },
                }
            },
            new()
            {
                Text = "It's over!",
                OnClickAction = () => { Debug.Log("I'm over!"); }
            }
        }
    };

    void Start()
    {
        var dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.TriggerDialogue(_dialogueLine);
    }
}