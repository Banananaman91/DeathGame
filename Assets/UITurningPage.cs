using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurningPage : MonoBehaviour
{

    //Set up a new Boolean parameter in the Unity Animator and name it, in this case “Jump”.
//Set up transitions between each state that the animation could follow. For example, the player could be running or idle before they jump, so both would need transitions into the animation.
//If the “Jump” boolean is set to true at any point, the m_Animator plays the animation. However, if it is ever set to false, the animation would return to the appropriate state (“Idle”).
//This script enables and disables this boolean in this case by listening for the mouse click or a tap of the screen.
    Animator m_Animator;
    bool TPageL2R;
    bool TPageR2L;

    void Start()
    {
        //This gets the Animator, which should be attached to the GameObject you are intending to animate.
        m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool("turnPageL2R", false);
        m_Animator.SetBool("turnPageR2L", false);
        TPageL2R = false;
        TPageR2L = false;
    }

    void Update()
    {
        //Click the mouse or tap the screen to change the animation
        if (Input.GetKey(KeyCode.Alpha2))
        {
            TPageL2R = true;
            TPageR2L = false;
        }
         
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TPageR2L = true;
            TPageL2R = false;
        }
      


        if (TPageL2R == true)
        {
            m_Animator.SetBool("turnPageL2R", true);
            m_Animator.SetBool("turnPageR2L", false);
        }


        if (TPageR2L == true)
        {
            m_Animator.SetBool("turnPageR2L", true);
            m_Animator.SetBool("turnPageL2R", false);
        }

    }
}

