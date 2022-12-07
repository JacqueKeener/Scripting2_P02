using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameUIController : MonoBehaviour
{
    [SerializeField] Text enemyThinkingTextUI = null;
    [SerializeField] GameObject winTextUI = null;
    [SerializeField] GameObject loseTextUI = null;
    [SerializeField] GameObject tieTextUI = null;

    private void OnEnable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan += OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded += OnEnemyTurnEnded;
    }

    private void OnDisable()
    {
        EnemyTurnCardGameState.EnemyTurnBegan -= OnEnemyTurnBegan;
        EnemyTurnCardGameState.EnemyTurnEnded -= OnEnemyTurnEnded;
    }

    private void Start()
    {
        enemyThinkingTextUI.gameObject.SetActive(false);
        winTextUI.gameObject.SetActive(false);
        loseTextUI.gameObject.SetActive(false);
        tieTextUI.gameObject.SetActive(false);
    }

    private void OnEnemyTurnBegan()
    {
        enemyThinkingTextUI.gameObject.SetActive(true);
    }

    private void OnEnemyTurnEnded()
    {
        enemyThinkingTextUI.gameObject.SetActive(false);
    }


}
