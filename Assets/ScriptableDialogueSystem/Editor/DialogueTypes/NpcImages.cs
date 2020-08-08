using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    [CreateAssetMenu(fileName = "NpcImages", menuName = "NpcImages", order = 2), Serializable]
    public class NpcImages : ScriptableObject
    {
        [SerializeField] private List<NpcBio> _npcImage;
        public List<NpcBio> NpcImage => _npcImage;
    }
}
