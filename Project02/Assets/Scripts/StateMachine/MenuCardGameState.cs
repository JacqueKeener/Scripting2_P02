using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCardGameState : CardGameState
{

    [SerializeField] GameObject menuGraphic;

    public override void Enter()
    {
        stateMachine.Input.PressedConfirm += OnPressedConfirm;
    }

    void OnPressedConfirm()
    {
        stateMachine.ChangeState<SetupCardGameState>();
    }


    public override void Exit()
    {
        stateMachine.Input.PressedConfirm -= OnPressedConfirm;
        menuGraphic.gameObject.SetActive(false);
    }
}
