using System;
using System.Linq;
using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private RenderDialogue[] _renderDialogues;
        // Start is called before the first frame update
        private void Awake()
        {
            foreach (var renderDialogue in _renderDialogues)
            {
                if (renderDialogue.gameObject.activeSelf) renderDialogue.gameObject.SetActive(false);
                foreach (var otherDialogue in _renderDialogues)
                {
                    if (otherDialogue == renderDialogue) continue;
                    renderDialogue.OtherDialogues.Add(otherDialogue);
                }
            }
        }
    }
}
