using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TieCardGameState : CardGameState
{
    [SerializeField] GameObject tieTextUI = null;

    public override void Enter()
    {
        Debug.Log("Tie Enter");
        tieTextUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {

    }

    void OnPressedConfirm()
    {
        //stateMachine.ChangeState<EnemyTurnCardGameState>();
    }
}
