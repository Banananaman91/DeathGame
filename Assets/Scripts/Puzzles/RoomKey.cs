using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System;
using DialogueScripts;

public class RoomKey : MonoBehaviour {

    [SerializeField] private GameObject _key;

    [SerializeField] Tilemap hiddenDoor;

    [SerializeField] private DeathMovement _movement;

    [SerializeField] private RenderDialogue _pageRender;

    [SerializeField] GameObject rayCast;

    [SerializeField] GameObject dialogueBox;

    [TextArea] public string[] _textBox;

    [TextArea] public string[] _unlockText;

    private BoxCollider2D _myCollider;

    bool movemm = false; //Book Case Movement/any other object moving
    float originalY;      //Starting possition of the book case

    public AudioClip MTrack;
    public AudioSource TrackSource;

    void Start()
    {
        dialogueBox.SetActive(false);

        _myCollider = GetComponent<BoxCollider2D>();

        originalY = transform.position.y; //Starts the moement of the book case

        TrackSource.clip = MTrack;
    }

    void Update()
    {
        if (movemm)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.025f, transform.position.z);//Animates the book case and moves it slowly down
            if (originalY - transform.position.y < -2)

                movemm = false;

                
        }
        
    }

    public void Interact(DeathMovement playerInteraction)
    {
        _movement.enabled = false;
        if (_key != null)
        {
            if (_key.activeSelf == false) StartCoroutine(RunParagraphCycle());

            else StartCoroutine(RunUnlockCycle());
        }

        else StartCoroutine(RunParagraphCycle());
    }

    private TileBase SetTile(Tilemap tilemap, Vector2 cellWorldPos)
    {
        tilemap.SetTile(tilemap.WorldToCell(cellWorldPos), null);
        return null;
    }

    public IEnumerator Play(String pageText)
    {
        var sb = new StringBuilder();

        var letters = pageText.ToCharArray();

        foreach (var letter in letters)
        {
            sb.Append(letter);

            _pageRender.RenderPageText(sb.ToString());

            yield return new WaitForSeconds(0.0375f);
        }


        yield return null;
    }

    public IEnumerator RunParagraphCycle()
    {
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _textBox.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_textBox[paragraphCounter]));

            ++paragraphCounter;

            yield return new WaitForSeconds(0.0375f);

            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            yield return null;
        }

        if (currentRoutine != null) StopCoroutine(currentRoutine);

        dialogueBox.SetActive(false);

        _myCollider.enabled = false;

        rayCast.SetActive(true);

        _myCollider.enabled = true;

        _movement.enabled = true;

        
    }

    public IEnumerator RunUnlockCycle()
    {
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _unlockText.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_unlockText[paragraphCounter]));

            ++paragraphCounter;

            yield return new WaitForSeconds(0.0375f);

            while (!Input.GetKeyDown(KeyCode.E))
            {
                yield return null;
            }

            yield return null;
        }

        if (currentRoutine != null) StopCoroutine(currentRoutine);

        SetTile(hiddenDoor, gameObject.transform.position);

        dialogueBox.SetActive(false);

        _myCollider.enabled = false;

        rayCast.SetActive(true);

        _myCollider.enabled = true;

        _movement.enabled = true;

        movemm = true;

        TrackSource.Play();


    }
}
