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



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void AddPage(GameObject _page)
    {
        Dialogue dialogueScr = _page.GetComponent<Dialogue>();
        SpriteRenderer spriteRdr = _page.GetComponent<SpriteRenderer>();
        switch (dialogueScr._itemClass)
        {
            case 1:
                switch (dialogueScr._pageClass)
                {
                    case 0:
                        GoodPages[0] = _page;
                        pageItemPlace[0].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[0].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 1:
                        GoodPages[1] = _page;
                        pageItemPlace[1].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[1].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 2:
                        GoodPages[2] = _page;
                        pageItemPlace[2].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[2].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 3:
                        GoodPages[3] = _page;
                        pageItemPlace[3].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[3].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                }
                break;
            case 2:
                switch (dialogueScr._pageClass)
                {
                    case 0:
                        EvilPages[0] = _page;
                        pageItemPlace[4].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[4].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 1:
                        EvilPages[1] = _page;
                        pageItemPlace[5].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[5].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 2:
                        EvilPages[2] = _page;
                        pageItemPlace[6].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[6].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 3:
                        EvilPages[3] = _page;
                        pageItemPlace[7].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[7].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                }
                break;
            case 3:
                switch (dialogueScr._pageClass)
                {
                    case 0:
                        DeceitfulPages[0] = _page;
                        pageItemPlace[8].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[8].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 1:
                        DeceitfulPages[1] = _page;
                        pageItemPlace[9].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[9].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 2:
                        DeceitfulPages[2] = _page;
                        pageItemPlace[10].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[10].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 3:
                        DeceitfulPages[3] = _page;
                        pageItemPlace[11].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[11].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                }

                break;
            case 4:
                switch (dialogueScr._pageClass)
                {
                    case 0:
                        UnfortunatePages[0] = _page;
                        pageItemPlace[12].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[12].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 1:
                        UnfortunatePages[1] = _page;
                        pageItemPlace[13].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[13].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 2:
                        UnfortunatePages[2] = _page;
                        pageItemPlace[14].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[14].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                    case 3:
                        UnfortunatePages[3] = _page;
                        pageItemPlace[15].image.overrideSprite = _page.GetComponent<SpriteRenderer>().sprite;
                        pageItemPlace[15].onClick.AddListener(() => dialogueScr.Interact(_thePlayer));
                        break;
                }

                break;
        }
    }

    private void CheckPage()
    {

    }
        
}

