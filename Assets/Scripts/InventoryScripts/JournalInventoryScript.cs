using System.Collections;
using System.Collections.Generic;
using Dialogue_scripts;
using UnityEngine;
using UnityEngine.UI;

public class JournalInventoryScript : MonoBehaviour
{
    [SerializeField] private Button[] pageItemPlace;
    [SerializeField] public GameObject[] GoodPages;
    [SerializeField] public GameObject[] EvilPages;
    [SerializeField] public GameObject[] DeceitfulPages;
    [SerializeField] public GameObject[] UnfortunatePages;
    public ManagerScript manager;                    //reference to ManagerScript
    [SerializeField] private DeathMovement _thePlayer;
    [SerializeField] private Test _testPlayer;
    [SerializeField] public GameObject[] _books;

    public void AddPage(GameObject _page)
    {
        Dialogue dialogueScr = _page.GetComponent<Dialogue>();
        Sprite spriteRdr = _page.GetComponent<SpriteRenderer>().sprite;

        _books[dialogueScr.pageClass] = _page;
        pageItemPlace[dialogueScr.pageClass].image.overrideSprite = spriteRdr;
        pageItemPlace[dialogueScr.pageClass].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
    }
}

