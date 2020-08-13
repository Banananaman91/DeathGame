using System;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [Serializable]
    public class MoodImage
    {
        [SerializeField] private Image _npcMoodImage;

        public Image NpcMoodImage
        {
            get => _npcMoodImage;
            set => _npcMoodImage = value;
        }
    }
}
