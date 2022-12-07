using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCardGameState : CardGameState
{
    [SerializeField] GameObject loseTextUI = null;

    public override void Enter()
    {
        Debug.Log("Lose Enter");
        loseTextUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {

    }

    void OnPressedConfirm()
    {
        //stateMachine.ChangeState<EnemyTurnCardGameState>();
    }
}
