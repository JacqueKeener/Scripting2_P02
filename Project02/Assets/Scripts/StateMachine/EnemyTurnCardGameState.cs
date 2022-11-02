using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float pauseDuration = 1.5f;

    public override void Enter()
    {
        Debug.Log("Enemy Enter");
        EnemyTurnBegan?.Invoke();

        StartCoroutine(EnemyThinkingRoutine(pauseDuration));
    }

    public override void Exit()
    {
        Debug.Log("Enemy Exit");
    }

    IEnumerator EnemyThinkingRoutine(float pause)
    {
        Debug.Log("Enemy Thinking");
        yield return new WaitForSeconds(pause);
        Debug.Log("Enemy performs actions");
        EnemyTurnEnded?.Invoke();
        stateMachine.ChangeState<PlayerTurnCardGameState>();
    }

}
