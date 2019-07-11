using DialogueScripts;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class JournalInventoryScript : MonoBehaviour
    {
        [SerializeField] private Button[] pageItemPlace;
        [SerializeField] private DeathMovement _thePlayer;
        [SerializeField] private GameObject[] _books;
        public GameObject[] Books => _books;

        public void AddPage(Dialogue page)
        {
            _books[page.PageClass] = page.gameObject;
            pageItemPlace[page.PageClass].image.overrideSprite = page.Sprite;
            pageItemPlace[page.PageClass].onClick.AddListener(() => page.Interact(_thePlayer));
        }
    }
}

