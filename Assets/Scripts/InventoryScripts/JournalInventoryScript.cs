using MovementNEW;
using Pages;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryScripts
{
    public class JournalInventoryScript : MonoBehaviour
    {
        [SerializeField] private Button[] _pageItemPlace;
        [SerializeField] private GameObject[] _books;
        [SerializeField] private PlayerMovement _thePlayer;
        public GameObject[] Books => _books;
        
        public void AddPage(Page page)
        {
            _books[page.PageClass] = page.gameObject;
            _pageItemPlace[page.PageClass].image.overrideSprite = page.SpriteObject;
            _pageItemPlace[page.PageClass].onClick.AddListener(() => page.Interact(_thePlayer));
        }
    }
}

