using System.Collections;
using System.Collections.Generic;
using DialogueScripts;
using UnityEngine;
using UnityEngine.UI;

public class JournalInventoryScript : MonoBehaviour
{
    [SerializeField] private Button[] pageItemPlace;
    [SerializeField] private DeathMovement _thePlayer;
    [SerializeField] private GameObject[] _books;
    public GameObject[] Books => _books;

    public void AddPage(GameObject _page)
    {
        Dialogue dialogue = _page.GetComponent<Dialogue>();
        Sprite sprite = _page.GetComponent<SpriteRenderer>().sprite;

        _books[dialogue.PageClass] = _page;
        pageItemPlace[dialogue.PageClass].image.overrideSprite = sprite;
        pageItemPlace[dialogue.PageClass].onClick.AddListener(() => dialogue.Interact(_thePlayer));
    }
}

