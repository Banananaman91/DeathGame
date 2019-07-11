using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using DialogueScripts;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class Mastermind : MonoBehaviour
{
    [SerializeField] Image[] bookImages;
    [SerializeField] GameObject PuzzlePanel;
    [SerializeField] Text numberOfRight;
    Color[] colorOrder;
    int book0, book1, book2, book3; //chosenColors
    int bookGuess0, bookGuess1, bookGuess2, bookGuess3; //selectedColors
    int howManyRight = 0;
    int currentCount = 0;
  

    [SerializeField] private DeathMovement movement;

    [SerializeField] private RenderDialogue _pageRender;

    [SerializeField] GameObject rayCast;

    [SerializeField] GameObject dialogueBox;

    [TextArea] public string[] _textBox;

    [TextArea] public string[] _winText;

    [TextArea] public string[] _loseText;

    BoxCollider2D _myCollider;

    [SerializeField] Tilemap hiddenDoor;

    bool movemm = false; //Book Case Movement
    float originalY;      //Starting possition of the book case
    public AudioClip MTrack; //Music track
    public AudioSource TrackSource; //Audio source located on the book case or any other moving object



    // Use this for initialization
    void Start()
    {
        colorOrder = new Color[4];
        colorOrder[0] = Color.white;
        colorOrder[1] = Color.yellow;
        colorOrder[2] = Color.cyan;
        colorOrder[3] = Color.magenta;

        //book0 = UnityEngine.Random.Range(0, 4);
        //book1 = UnityEngine.Random.Range(0, 4);
        //book2 = UnityEngine.Random.Range(0, 4);
        //book3 = UnityEngine.Random.Range(0, 4);

        dialogueBox.SetActive(false);

        _myCollider = GetComponent<BoxCollider2D>();
        originalY = transform.position.y; //Starts the moement of the book case

        TrackSource.clip = MTrack; // Music track plays


    }

    // Update is called once per frame
    void Update()
    {

        if (movemm)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.025f, transform.position.z);//Animates the book case and moves it slowly down
            if (originalY - transform.position.y > 2)
                movemm = false;
        }

        if (PuzzlePanel.activeSelf != false)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) //selectedColors    compare peg to pegGuess
            {
                bookGuess0++;
                if (bookGuess0 > 3)
                {
                    bookGuess0 = 0;
                    bookImages[0].color = colorOrder[0];
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2)) //selectedColors    compare peg to pegGuess
            {
                bookGuess1++;
                if (bookGuess1 > 3)
                {
                    bookGuess1 = 0;
                    bookImages[1].color = colorOrder[0];
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3)) //selectedColors    compare peg to pegGuess
            {
                bookGuess2++;
                if (bookGuess2 > 3)
                {
                    bookGuess2 = 0;
                    bookImages[2].color = colorOrder[0];
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4)) //selectedColors    compare peg to pegGuess
            {
                bookGuess3++;
                if (bookGuess3 > 3)
                {
                    bookGuess3 = 0;
                    bookImages[3].color = colorOrder[0];
                }

            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckColors();
            }
            UpdateColors();
        }
        

    }


    void CheckColors()
    {
        howManyRight = 0;
        if (bookGuess0 == book0)
        {
            howManyRight++;
        }
        if (bookGuess1 == book1)
        {
            howManyRight++;
        }
        if (bookGuess2 == book2)
        {
            howManyRight++;
        }
        if (bookGuess3 == book3)
        {
            howManyRight++;
        }
        if (howManyRight == 4)
        {
            Debug.Log("Yes Yes Yes Yes Yes");
            movement.enabled = true;
            PuzzlePanel.SetActive(false);
            _myCollider.enabled = true;
            currentCount = 0;
            SetTile(hiddenDoor, gameObject.transform.position);
            print("I PLAY ANIMATION");
            StartCoroutine(RunWinCycle());
            movemm = true;
            TrackSource.Play();//Sound PLays after the mastermind puszzle is solved
        }
        if (currentCount == 12)
        {
            Debug.Log("ZA WARUDO!");
            movement.enabled = true;
            PuzzlePanel.SetActive(false);
            _myCollider.enabled = true;
            currentCount = 0;
            StartCoroutine(RunLoseCycle());
        }

        currentCount++;
        numberOfRight.text = howManyRight.ToString();
    }



    void UpdateColors()
    {
        bookImages[0].color = colorOrder[bookGuess0];
        bookImages[1].color = colorOrder[bookGuess1];
        bookImages[2].color = colorOrder[bookGuess2];
        bookImages[3].color = colorOrder[bookGuess3];
    }



    public void Interact(DeathMovement playerInteraction)
    {
        book0 = UnityEngine.Random.Range(0, 4);
        book1 = UnityEngine.Random.Range(0, 4);
        book2 = UnityEngine.Random.Range(0, 4);
        book3 = UnityEngine.Random.Range(0, 4);
        movement.enabled = false;
        StartCoroutine(RunParagraphCycle());
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

        PuzzlePanel.SetActive(true);

    }

    public IEnumerator RunWinCycle()
    {
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _winText.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_winText[paragraphCounter]));

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
    }

    public IEnumerator RunLoseCycle()
    {
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _loseText.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_loseText[paragraphCounter]));

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
    }

    private TileBase SetTile(Tilemap tilemap, Vector2 cellWorldPos)
    {
        tilemap.SetTile(tilemap.WorldToCell(cellWorldPos),null);
        return null;
    }
}

