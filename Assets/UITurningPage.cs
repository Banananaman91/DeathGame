using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurningPage : MonoBehaviour
{

    //Set up a new Boolean parameter in the Unity Animator and name it, in this case “Jump”.
//Set up transitions between each state that the animation could follow. For example, the player could be running or idle before they jump, so both would need transitions into the animation.
//If the “Jump” boolean is set to true at any point, the m_Animator plays the animation. However, if it is ever set to false, the animation would return to the appropriate state (“Idle”).
//This script enables and disables this boolean in this case by listening for the mouse click or a tap of the screen.
    [SerializeField] private KeyCode _turnLtoR;
    [SerializeField] private KeyCode _turnRtoL;
    Animator _mAnimator;
    private static readonly int TurnPageL2R = Animator.StringToHash("turnPageL2R");
    private static readonly int TurnPageR2L = Animator.StringToHash("turnPageR2L");

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        _mAnimator = gameObject.GetComponent<Animator>();
        _mAnimator.SetBool(TurnPageL2R, false);
        _mAnimator.SetBool(TurnPageR2L, false);
    }

    void Update()
    {
        //Click the mouse or tap the screen to change the animation
        if (Input.GetKey(_turnLtoR)) TurnLtoR();
        if (Input.GetKeyDown(_turnRtoL)) TurnRtoL();
    }

    private void TurnLtoR()
    {
        _mAnimator.SetBool(TurnPageL2R, true);
        _mAnimator.SetBool(TurnPageR2L, false);
    }

    private void TurnRtoL()
    {
        _mAnimator.SetBool(TurnPageL2R, false);
        _mAnimator.SetBool(TurnPageR2L, true);
    }
}

