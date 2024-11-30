using UnityEngine;
using Dialogues;

public class BananaEatingTrigger : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    
    private readonly DialogueLine _dialogueLine = new DialogueLine
    {
        CharacterName = "Executioner",
        Text = "[taking out a banana] Mmm, tasty!",
        Answers = new DialogueAnswer[]
        {
            new()
            {
                Text = "You probably shouldn't be eating on a job...",
                NextDialogueLine = new DialogueLine
                {
                    CharacterName = "Executioner",
                    Text = "Nobody asked your opinion.",
                    Answers = new DialogueAnswer[]
                    {
                        new()
                        {
                            Text = "...",
                            OnClickAction = EatBanana
                        }
                    },
                }
            },
        },
    };


    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public void TriggerAnnouncement(ItemData _tool) {
        dialogueTrigger.TriggerDialogue(_dialogueLine);
    }

    static void EatBanana() {
        GameObject executionerObject = GameObject.Find("Executioner");
        Executioner executioner = executionerObject.GetComponent<Executioner>();
        executioner.EatBanana();
    }
}
