using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITurningPage : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int TurnPage1 = Animator.StringToHash("TurnPage1");
    private static readonly int TurnPage2 = Animator.StringToHash("TurnPage2");

    void Start()
    {
        _animator.SetBool(TurnPage1, false);
        _animator.SetBool(TurnPage2, false);
    }

    public void TurnLtoR(int _page) //int 0 manages page 1, int 1 manages page 2
    {
        var animId = _page == 0 ? TurnPage1 : TurnPage2;
        _animator.SetBool(animId, true);
    }

    public void TurnRtoL(int _page)
    {
        var animId = _page == 0 ? TurnPage1 : TurnPage2;
        _animator.SetBool(animId, false);
    }
}

