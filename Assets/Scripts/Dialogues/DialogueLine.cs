namespace Dialogues
{
    public class DialogueLine
    {
        public string CharacterName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DialogueAnswer[] Answers { get; set; }
    }
}