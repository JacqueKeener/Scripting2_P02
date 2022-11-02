using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{

    [SerializeField] Text playerTurnTextUI = null;

    int playerTurnCount = 0;

    public override void Enter()
    {
        Debug.Log("Player Enter");
        playerTurnTextUI.gameObject.SetActive(true);
        playerTurnCount++;
        playerTurnTextUI.text = "Player Turn: " + playerTurnCount.ToString();
        stateMachine.Input.PressedConfirm += OnPressedConfirm;
        stateMachine.Input.PressedLeft += OnPressedLeft;
        stateMachine.Input.PressedRight += OnPressedRight;
        stateMachine.Input.PressedCancel+= OnPressedCancel;
    }

    public override void Exit()
    {
        playerTurnTextUI.gameObject.SetActive(false);
        stateMachine.Input.PressedConfirm -= OnPressedConfirm;
        stateMachine.Input.PressedLeft -= OnPressedLeft;
        stateMachine.Input.PressedRight -= OnPressedRight;
        stateMachine.Input.PressedCancel -= OnPressedCancel;
        Debug.Log("Player Exit");
    }

    void OnPressedConfirm()
    {
        if (stateMachine.deckHasCards)
        {
            stateMachine.ChangeState<EnemyTurnCardGameState>();
        }
        else
        {
            if(stateMachine.leftScore > stateMachine.rightScore)
            {
                stateMachine.ChangeState<WinCardGameState>();
            }
            else
            {
                stateMachine.ChangeState<LoseCardGameState>();
            }
        }
    }

    void OnPressedLeft()
    {
        stateMachine.leftScore++;
    }

    void OnPressedRight()
    {
        stateMachine.rightScore++;
    }

    void OnPressedCancel()
    {
        stateMachine.deckHasCards = !stateMachine.deckHasCards;
    }
}
