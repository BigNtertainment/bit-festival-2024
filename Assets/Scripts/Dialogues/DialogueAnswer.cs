using System;
using JetBrains.Annotations;

namespace Dialogues
{
    public class DialogueAnswer
    {
        public string Text { get; set; } = string.Empty;

        [CanBeNull]
        public DialogueLine NextDialogueLine { get; set; }

        [CanBeNull]
        public Action OnClickAction { get; set; }
    }
}