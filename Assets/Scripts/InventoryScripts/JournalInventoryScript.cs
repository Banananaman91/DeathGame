using MovementNEW;
using Pages;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class JournalInventoryScript : MonoBehaviour
    {
        [SerializeField] private Button[] pageItemPlace;
        [SerializeField] private PlayerMovement _thePlayer;
        [SerializeField] private GameObject[] _books;
        public GameObject[] Books => _books;

        public void AddPage(Page page)
        {
            _books[page.PageClass] = page.gameObject;
            pageItemPlace[page.PageClass].image.overrideSprite = page.SpriteObject;
            pageItemPlace[page.PageClass].onClick.AddListener(() => page.Interact(_thePlayer));
        }
    }
}

