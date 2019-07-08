using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalInventoryScript : MonoBehaviour {

    public Button[] journalItemPlace;                //creates an array of Buttons
    public Button[] pageItemPlace;
    public GameObject[] GoodPages;
    public GameObject[] EvilPages;
    public GameObject[] DeceitfulPages;
    public GameObject[] UnfortunatePages;
    public ManagerScript manager;                    //reference to ManagerScript
    [SerializeField] private DeathMovement _thePlayer;
    [SerializeField] private Test _testPlayer;
    [SerializeField] GameObject[] _books;

    public void AddPage(GameObject _page)
    {
        Dialogue dialogueScr = _page.GetComponent<Dialogue>();
        Sprite spriteRdr = _page.GetComponent<SpriteRenderer>().sprite;

        _books[dialogueScr._pageClass] = _page;
        pageItemPlace[dialogueScr._pageClass].image.overrideSprite = spriteRdr;
        pageItemPlace[dialogueScr._pageClass].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
    }
}

