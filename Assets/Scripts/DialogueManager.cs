using System.Collections.Generic;
using Dialogues;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject answerButtonPrefab;

    // Used to show and hide dialogue windows.
    public Transform dialogueWindow;

    public TextMeshProUGUI dialogueCharacterNameText;
    public TextMeshProUGUI dialogueContentText;

    [CanBeNull] private DialogueLine _currentDialogueLine;
    private readonly List<GameObject> _instantiatedButtons = new();

    public void StartDialogue(DialogueLine dialogueLine)
    {
        _currentDialogueLine = dialogueLine;
        DisplayCurrentDialogueLine();
        ShowDialogUI();
    }

    private void DisplayCurrentDialogueLine()
    {
        if (_currentDialogueLine is { } dialogueLine)
        {
            DisplayDialogueLine(dialogueLine);
        }
    }

    private void DisplayDialogueLine(DialogueLine dialogueLine)
    {
        dialogueCharacterNameText.text = dialogueLine.CharacterName;
        dialogueContentText.text = dialogueLine.Text;
        DisplayAnswerButtons(dialogueLine.Answers);
    }

    private void DisplayAnswerButtons(DialogueAnswer[] dialogueAnswers)
    {
        // Destroy all existing buttons
        foreach (var answerButton in _instantiatedButtons)
        {
            Destroy(answerButton.gameObject);
        }

        _instantiatedButtons.Clear();

        // Create new buttons
        foreach (var dialogueAnswer in dialogueAnswers)
        {
            var dialogAnswerButton = CreateAnswerButton(dialogueAnswer);
            _instantiatedButtons.Add(dialogAnswerButton);
        }
    }

    private GameObject CreateAnswerButton(DialogueAnswer dialogueAnswer)
    {
        var dialogueAnswerButton = Instantiate(answerButtonPrefab, dialogueWindow);
        dialogueAnswerButton.GetComponentInChildren<TextMeshProUGUI>().text = dialogueAnswer.Text;
        dialogueAnswerButton.GetComponent<Button>().onClick.AddListener(() => OnAnswerSelected(dialogueAnswer));
        return dialogueAnswerButton;
    }

    private void OnAnswerSelected(DialogueAnswer dialogueAnswer)
    {
        if (dialogueAnswer.NextDialogueLine is { } nextDialogueLine)
        {
            _currentDialogueLine = nextDialogueLine;
            DisplayCurrentDialogueLine();
        }
        else
        {
            HideDialogUI();
            _currentDialogueLine = null;
        }

        dialogueAnswer.OnClickAction?.Invoke();
    }

    private void ShowDialogUI()
    {
        dialogueWindow.gameObject.SetActive(true);
    }

    private void HideDialogUI()
    {
        dialogueWindow.gameObject.SetActive(false);
    }

    public bool IsDialogueRunning()
    {
        return _currentDialogueLine is not null;
    }
}