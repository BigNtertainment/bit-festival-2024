using UnityEngine;
using Dialogues;

public class AnnouncementTrigger : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    
    private readonly DialogueLine _dialogueLine = new DialogueLine
    {
        CharacterName = "Executioner",
        Text = "Ekhm, hello everyone...",
        Answers = new DialogueAnswer[]
        {
            new()
            {
                Text = "[Continue]",
                NextDialogueLine = new DialogueLine
                {
                    CharacterName = "Executioner",
                    Text = "This here witch has been sentenced to hanging for using the forbidden time magic. Do you have anything to say?",
                    Answers = new DialogueAnswer[]
                    {
                        new()
                        {
                            Text = "I'm innocent!",
                            NextDialogueLine = new DialogueLine {
                                CharacterName = "Executioner",
                                Text = "Yeah, everyone's innocent once they're on the gallows. Prepare to hang!",
                                Answers = new DialogueAnswer[] {
                                    new() {
                                        Text = "[prepare to hang]",
                                        OnClickAction = ChangeExecutionerState
                                    }
                                }
                            }
                        },
                        new()
                        {
                            Text = "No",
                            NextDialogueLine = new DialogueLine {
                                CharacterName = "Executioner",
                                Text = "Oh. Ok. I'm still hanging you though.",
                                Answers = new DialogueAnswer[] {
                                    new() {
                                        Text = ":(",
                                        OnClickAction = ChangeExecutionerState
                                    },
                                }
                            }
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

    static void ChangeExecutionerState() {
        GameObject executionerObject = GameObject.Find("Executioner");
        Executioner executioner = executionerObject.GetComponent<Executioner>();
        executioner.currentState = Executioner.State.Eating;
    }
}
