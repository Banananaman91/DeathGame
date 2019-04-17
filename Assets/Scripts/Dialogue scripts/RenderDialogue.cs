using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RenderDialogue : MonoBehaviour {
    public Text _pageText; // assign textbox here in the inspector

    public void RenderPageText(string pageText) // method to render text to textbox
    {
        _pageText.text = pageText; // uses value of pageText string to print to text box
    }
}
