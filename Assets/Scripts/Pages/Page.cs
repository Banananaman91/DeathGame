using MovementNEW;
using ScriptableDialogueSystem.Editor.DialogueTypes;
using UnityEngine;

namespace Pages
{
    public class Page : DialogueObject, IInteract
    {
        [SerializeField] private int _pageClass;
        [SerializeField] private Sprite _spriteObject;
        [TextArea]
        [SerializeField] private string _clue;

        public int PageClass => _pageClass;
        public Sprite SpriteObject => _spriteObject;
        public string Clue => _clue;
        public void Interact(PlayerMovement playerInteraction)
        {
            _pageRender.PlayDialogue(_myDialogue);
        }
    }
}
