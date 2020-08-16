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
        [SerializeField] private List<MoodImage> _npcMoodImages;
        [SerializeField] private Sprite _dialogueBackgroundImage;
        [SerializeField] private Color _dialogueBackgroundColour;
        [SerializeField] private Color _dialogueTextColour;
        [SerializeField] private Font _dialogueTextFont;
        public string NpcName
        {
            get => _npcName;
            set => _npcName = value;
        }

        public List<MoodImage> NpcMoodImages
        {
            get => _npcMoodImages;
            set => _npcMoodImages = value;
        }

        public Sprite DialogueBackgroundImage
        {
            get => _dialogueBackgroundImage;
            set => _dialogueBackgroundImage = value;
        }

        public Color DialogueTextColour
        {
            get => _dialogueTextColour;
            set => _dialogueTextColour = value;
        }

        public Font DialogueTextFont
        {
            get => _dialogueTextFont;
            set => _dialogueTextFont = value;
        }

        public Color DialogueBackgroundColour
        {
            get => _dialogueBackgroundColour;
            set => _dialogueBackgroundColour = value;
        }
    }
}
