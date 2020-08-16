﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [CreateAssetMenu(fileName = "NpcImages", menuName = "Dialogue/Mood Images", order = 2), Serializable]
    public class NpcImages : ScriptableObject
    {
        [SerializeField] private List<NpcBio> _npcImage;
        public List<NpcBio> NpcImage
        {
            get => _npcImage;
            set => _npcImage = value;
        }
    }
}
