using UnityEngine;

namespace DialogueTypes
{
    public class DialogueObject : MonoBehaviour
    {
        [SerializeField] private DialogueTypes.Dialogue _dialogue;
        [SerializeField] private NpcImages _npcImages;
        [SerializeField] private RenderDialogue _pageRender;
        [SerializeField] private int _startMessage;
        public int PageClass { get; set; }
        public Sprite SpriteObject { get; set; }

        public void Select() => _pageRender.PlayParagraphCycle(_dialogue, _npcImages,_startMessage);
        

        public void DeSelect()
        {
            
        }

        public void Interact(DeathMovement thePlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}
