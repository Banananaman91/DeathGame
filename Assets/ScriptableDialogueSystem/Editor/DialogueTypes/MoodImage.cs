using System;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [Serializable]
    public class MoodImage
    {
        [SerializeField] private Sprite _npcMoodImage;

        public Sprite NpcMoodImage
        {
            get => _npcMoodImage;
            set => _npcMoodImage = value;
        }
    }
}
