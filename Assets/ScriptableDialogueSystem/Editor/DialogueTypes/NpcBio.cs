using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [Serializable]
    public class NpcBio
    {
        [SerializeField] private string _npcName;
        [SerializeField] private List<Image> _npcMoodImages;
        [SerializeField] private Sprite _dialogueBackgroundImage;
        [SerializeField] private Color _dialogueBackgroundColour;
        [SerializeField] private Color _dialogueTextColour;
        [SerializeField] private Font _dialogueTextFont;
        public string NpcName => _npcName;
        public List<Image> NpcMoodImages => _npcMoodImages;

        public Sprite DialogueBackgroundImage => _dialogueBackgroundImage;

        public Color DialogueTextColour => _dialogueTextColour;

        public Font DialogueTextFont => _dialogueTextFont;

        public Color DialogueBackgroundColour => _dialogueBackgroundColour;
    }
}
