using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EnemyTurnCardGameState : CardGameState
{
    public static event Action EnemyTurnBegan;
    public static event Action EnemyTurnEnded;

    [SerializeField] float pauseDuration = 1.5f;
    [SerializeField] Hand enemyhand;
    [SerializeField] Deck gameDeck;
    [SerializeField] Pile matchPile;
    [SerializeField] Hand playerhand;
    [SerializeField] Pile playerPile;
    [SerializeField] Text numberGuess;
    [SerializeField] AudioClip stackCardSound;
    [SerializeField] AudioClip dealCardSound;
    [SerializeField] AudioClip thinkingSound;

    private List<Card> transporting = new List<Card>();
    private bool checking;

    int justDrawn = -1;
    

    private Queue<int> recentGuesses = new Queue<int>();

    public override void Enter()
    {
        Debug.Log("Enemy Enter");
        EnemyTurnBegan?.Invoke();

        

        if (!gameDeck.IsEmpty())
        {
            StartCoroutine(transportCard(gameDeck.DealCard(), false));
            AudioHelper.PlayClip2D(dealCardSound, 1f);
            StartCoroutine(EnemyThinkingRoutine(pauseDuration));
        }
        else
        {
            if (matchPile.score < playerPile.score)
            {
                stateMachine.ChangeState<WinCardGameState>();
            }
            else if (matchPile.score > playerPile.score)
            {
                stateMachine.ChangeState<LoseCardGameState>();
            }
            else
            {
                stateMachine.ChangeState<TieCardGameState>();
            }
        }
    }

    public override void Exit()
    {
        if (playerhand.GetHandSize() == 0 && enemyhand.GetHandSize() == 0 && gameDeck.IsEmpty())
        {
            if (matchPile.score < playerPile.score)
            {
                stateMachine.ChangeState<WinCardGameState>();
            }
            else if (matchPile.score > playerPile.score)
            {
                stateMachine.ChangeState<LoseCardGameState>();
            }
            else
            {
                stateMachine.ChangeState<TieCardGameState>();
            }
        }
        else
        {
            numberGuess.text = "";
            numberGuess.gameObject.SetActive(false);
            stateMachine.ChangeState<PlayerTurnCardGameState>();

            Debug.Log("Enemy Exit");
        }
    }

    IEnumerator EnemyThinkingRoutine(float pause)
    {
        Debug.Log("Enemy Thinking");
        AudioHelper.PlayClip2D(thinkingSound, 1f);
        yield return new WaitForSeconds(pause);
        Debug.Log("Enemy performs actions");
        EnemyTurnEnded?.Invoke();
        StartCoroutine(GuessValue(2f));
        
    }

    IEnumerator GuessValue(float pause)
    {
        while (enemyhand.IsMatchInHand() != null)
        {
            Card c = enemyhand.IsMatchInHand();
            c = enemyhand.RemoveCard(c.GetValue());
            Card m = enemyhand.RemoveCard(c.GetValue());
            StartCoroutine(transportCard(c, true));
            StartCoroutine(transportCard(m, true));
            matchPile.score++;
        }
        yield return new WaitForSeconds(pause);
        if(enemyhand.GetHandSize() != 0)
        {
            if(recentGuesses.Count == 0)
            {
                List<int> temp = new List<int>();
                for(int i = 0; i < enemyhand.GetHandSize(); i++)
                {
                    temp.Add(enemyhand.GetValue(i));
                }
                int n = temp.Count - 1;
                while (n > 0)
                {
                    int r = UnityEngine.Random.Range(0, temp.Count);
                    int c = temp[n];
                    temp[n] = temp[r];
                    temp[r] = c;
                    n--;
                }
                for (int i = 0; i < temp.Count; i++)
                {
                    recentGuesses.Enqueue(temp[i]);
                }
            }

            if (!recentGuesses.Contains(justDrawn) && enemyhand.IsInHand(justDrawn))
            {
                //Guess Decided Wait Topdeck
                //Yell guess
                numberGuess.gameObject.SetActive(true);
                numberGuess.text = justDrawn.ToString();
                yield return new WaitForSeconds(pause);
                if (TakeGuess(justDrawn))
                {

                }
                recentGuesses.Enqueue(justDrawn);
            }
            else
            {
                while (!enemyhand.IsInHand(recentGuesses.Peek()))
                {
                    recentGuesses.Dequeue();
                }
                //Guess Decided Wait In Hand
                //Yell guess
                numberGuess.gameObject.SetActive(true);
                numberGuess.text = recentGuesses.Peek().ToString();
                yield return new WaitForSeconds(pause);
                TakeGuess(recentGuesses.Peek());
                recentGuesses.Enqueue(recentGuesses.Dequeue());
            }
        }
        else
        {
            numberGuess.gameObject.SetActive(true);
            numberGuess.text = "I Pass";
            yield return new WaitForSeconds(pause);
            //PASS TURN
        }
        yield return new WaitForSeconds(pause);
        stateMachine.ChangeState<PlayerTurnCardGameState>();
    }

    private bool TakeGuess(int value)
    {
        
        
        Debug.Log(value);
        bool ret = false;
        for (int i = 0; i < playerhand.GetHandSize(); i++)
        {
            if (playerhand.GetValue(i) == value)
            {
                ret = true;
                StartCoroutine(transportCard(enemyhand.RemoveCard(value), true));
                StartCoroutine(transportCard(playerhand.RemoveCard(value), true));
                matchPile.score++;
            }
        }
        return ret;
    }

    private IEnumerator transportCard(Card c, bool toPile)
    {
        c.showNumber = false;
        if (toPile)
        {
            AudioHelper.PlayClip2D(stackCardSound, 1f);
            c.showNumber = true;
            c.SetBelongs(false);
            transporting.Add(c);
            c.start = c.transform.position;
            c.end = matchPile.GetStackPoint(matchPile.size);
            matchPile.size++;
            c.moving = true;
        }
        else
        {
            justDrawn = c.GetValue();
            checking = false;
            transporting.Add(c);
            c.start = c.transform.position;
            c.end = enemyhand.transform.position;
            c.moving = true;
        }

        yield return new WaitForSeconds(.5f);

        c.moving = false;
        c.transform.position = c.end;
        transporting.Remove(c);
        if (toPile)
        {
            matchPile.AddCard(c);
        }
        else
        {
            enemyhand.AddCard(c);
            c.SetBelongs(true);
            checking = true;
        }

    }

}
