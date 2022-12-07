using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCardGameState : CardGameState
{
    [SerializeField] GameObject winTextUI = null;

    public override void Enter()
    {
        Debug.Log("Win Enter");
        winTextUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {

    }

    void OnPressedConfirm()
    {
        //stateMachine.ChangeState<EnemyTurnCardGameState>();
    }
}
