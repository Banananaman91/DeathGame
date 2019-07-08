using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

[RequireComponent(typeof(BoxCollider2D))]
public class Dialogue : Interacted
{
    [TextArea] public string[] _textBox; // string array for inputting separate dialogue boxes

    [TextArea] public string[] _hiddenTextBox;

    [SerializeField] private RenderDialogue _pageRender;

    [SerializeField] GameObject rayCast;

    [SerializeField] GameObject dialogueBox;

    public JournalInventoryScript joInvScript;

    [SerializeField] GameObject accessItem;

    [SerializeField] GameObject hiddenItem;

    [SerializeField] GameObject item;

    [SerializeField] private DeathMovement _movement;

    public int _itemClass;

    public int _pageClass;

    BoxCollider2D _myCollider;

    SpriteRenderer _myRenderer;

    public bool isIntro;


    private void Start()
    {
        dialogueBox.SetActive(false);

        _myCollider = GetComponent<BoxCollider2D>();

        _myRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (hiddenItem != null) hiddenItem.SetActive(false);

        if (item != null) item.SetActive(false);
    }



    public void Interact(DeathMovement playerInteraction)
    {
        if (_movement != null) _movement.enabled = false;
        if (accessItem != null)
        {
            if (accessItem.activeSelf == false) StartCoroutine(RunParagraphCycle());

            else StartCoroutine(RunHiddenParagraphCycle());
        }

        else if (item != null)
        {
            if (item.activeSelf == true) StartCoroutine(RunHiddenParagraphCycle());

            else StartCoroutine(RunParagraphCycle());
        }

        else StartCoroutine(RunParagraphCycle());
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

        if (_itemClass >= 1)
        {
            joInvScript.AddPage(gameObject);

            _myRenderer.enabled = false;
        }

        _myCollider.enabled = false;

        rayCast.SetActive(true);

        if (_itemClass == 0)
        {
            _myCollider.enabled = true;
            if (item != null) item.SetActive(true);
            if (isIntro == true)
            {
                _myRenderer.enabled = false;
                _myCollider.enabled = false;
            }
        }
        if (_movement != null) _movement.enabled = true;
        
    }

    public IEnumerator RunHiddenParagraphCycle()
    {
        dialogueBox.SetActive(true);

        rayCast.SetActive(false);

        int paragraphCounter = 0;

        Coroutine currentRoutine = null;

        while (paragraphCounter < _hiddenTextBox.Length)
        {
            if (currentRoutine != null) StopCoroutine(currentRoutine);

            currentRoutine = StartCoroutine(Play(_hiddenTextBox[paragraphCounter]));

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

        if (hiddenItem != null) hiddenItem.SetActive(true);

        if (accessItem != null) accessItem = null;

        if (_movement != null) _movement.enabled = true;
    }
}
