using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int startingCardNumber = 5;
    [SerializeField] int numberOfPlayers = 2;

    bool activated = false;

    public override void Enter()
    {
        Debug.Log("Setup");
        activated = false;
    }

    public override void Tick()
    {
        if(activated == false)
        {
            activated = true;
            stateMachine.ChangeState<PlayerTurnCardGameState>();
        }
    }

    public override void Exit()
    {
        activated = false;
        Debug.Log("Setup Exit");
    }

}
