using UnityEngine;

namespace ScriptableDialogueSystem.Editor.DialogueTypes
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _renderDialogues;
        // Start is called before the first frame update
        private void Start()
        {
            foreach (var renderDialogue in _renderDialogues)
            {
                if (renderDialogue.activeSelf) renderDialogue.SetActive(false);
            }
        }
    }
}
