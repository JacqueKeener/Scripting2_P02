using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnCardGameState : CardGameState
{

    [SerializeField] Hand playerhand;
    [SerializeField] Deck gameDeck;
    [SerializeField] Pile matchPile;
    [SerializeField] Pile enemyPile;
    [SerializeField] Hand enemyhand;
    [SerializeField] Text goFish;
    [SerializeField] AudioClip stackCardSound;
    [SerializeField] AudioClip dealCardSound;


    public GameObject selector;
    private int hover = 0;

    private Card moveCardOne;
    private Card moveCardTwo;
    private IEnumerator cardMoveTrack;
    private Transform startOne;
    private Transform endOne;
    private Transform startTwo;
    private Vector3 endTwo;

    private List<Card> transporting = new List<Card>();
    private bool checking;

    int playerTurnCount = 0;

    public override void Enter()
    {
        moveCardOne = null;
        moveCardTwo = null;
        Debug.Log("Player Enter");
        playerTurnCount++;
        stateMachine.Input.PressedConfirm += OnPressedConfirm;
        stateMachine.Input.PressedLeft += OnPressedLeft;
        stateMachine.Input.PressedRight += OnPressedRight;
        stateMachine.Input.PressedCancel += OnPressedCancel;
        Debug.Log("Dealing");
        if (!gameDeck.IsEmpty())
        {
            cardMoveTrack = transportCard(gameDeck.DealCard(), false);
            StartCoroutine(cardMoveTrack);
            AudioHelper.PlayClip2D(dealCardSound, 1f);
        }
        else
        {
            if (matchPile.score > enemyPile.score)
            {
                stateMachine.ChangeState<WinCardGameState>();
                
            }
            else if (matchPile.score < enemyPile.score)
            {
                stateMachine.ChangeState<LoseCardGameState>();
                
            }
            else
            {
                stateMachine.ChangeState<TieCardGameState>();
                
            }
        }
        checking = false;
        hover = 0;
    }

    public override void Exit()
    {
        hover = 0;
        stateMachine.Input.PressedConfirm -= OnPressedConfirm;
        stateMachine.Input.PressedLeft -= OnPressedLeft;
        stateMachine.Input.PressedRight -= OnPressedRight;
        stateMachine.Input.PressedCancel -= OnPressedCancel;
        Debug.Log("Player Exit");
    }

    public override void Tick()
    {
        /*
        if (moveCardOne != null)
        {
            moveCardOne.transform.Translate((endOne.position - startOne.position).normalized * (Vector3.Distance(startOne.position, endOne.position) * Time.deltaTime * 2f));
        }
        if (moveCardTwo != null)
        {
            moveCardTwo.transform.Translate((endTwo - startTwo.position).normalized * (Vector3.Distance(startTwo.position, endTwo) * Time.deltaTime * 2f));
        }
        */
        if (checking)
        {
            while (playerhand.IsMatchInHand() != null)
            {
                Card c = playerhand.IsMatchInHand();
                c = playerhand.RemoveCard(c.GetValue());
                Card m = playerhand.RemoveCard(c.GetValue());
                StartCoroutine(transportCard(c, true));
                StartCoroutine(transportCard(m, true));
                matchPile.score++;
            }
        }
    }

    private void Update()
    {
        if (playerhand.GetHandSize() != 0)
        {
            selector.transform.position = playerhand.GetCardPosition(hover);
            selector.transform.Translate(0f, 0f, 0f + .01f);
        }
        else
        {
            selector.transform.position = new Vector3(-10f, -10f, -10f);
        }
    }


    void OnPressedConfirm()
    {
        /*
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
        */
        if(playerhand.GetHandSize() != 0)
        {
            if (!TakeGuess(playerhand.GetValue(hover)))
            {
                StartCoroutine(GoFishDelay(1.5f));
            }
        }
        if (playerhand.GetHandSize() == 0 && enemyhand.GetHandSize() == 0 && gameDeck.IsEmpty())
        {
            if(matchPile.score > enemyPile.score)
            {
                stateMachine.ChangeState<WinCardGameState>();
            }
            else if (matchPile.score < enemyPile.score)
            {
                stateMachine.ChangeState<LoseCardGameState>();
            }
            else
            {
                stateMachine.ChangeState<TieCardGameState>();
            }
        }
        stateMachine.ChangeState<EnemyTurnCardGameState>();
    }

    private bool TakeGuess(int value)
    {
        bool ret = false;
        for (int i = 0; i < enemyhand.GetHandSize(); i++)
        {
            if (enemyhand.GetValue(i) == value)
            {
                ret = true;
                StartCoroutine(transportCard(enemyhand.RemoveCard(value), true));
                StartCoroutine(transportCard(playerhand.RemoveCard(value), true));
                matchPile.score++;
            }
        }
        return ret;
    }

    void OnPressedLeft()
    {
        //stateMachine.leftScore++;
        hover = Mathf.Max(hover - 1, 0);
    }

    void OnPressedRight()
    {
        //stateMachine.rightScore++;
        hover = Mathf.Min(hover + 1, playerhand.GetHandSize()-1);
    }

    void OnPressedCancel()
    {
        stateMachine.deckHasCards = !stateMachine.deckHasCards;
    }
    /*
    private IEnumerator transportCard(Card c, bool drawing, int ind)
    {
        //yield return new WaitForSeconds(1f);
        if (!drawing)
        {
            //playerhand.RemoveCard(c.GetValue());
            //c.SetBelongs(false);
        }

        if (ind == 1)
        {
            moveCardOne = c;
        }

        if (ind == 2)
        {
            moveCardTwo = c;
        }


        yield return new WaitForSeconds(.5f);

        if(ind == 1)
        {
            moveCardOne.transform.position = endOne.position;
            moveCardOne = null;
        }

        if (ind == 2)
        {
            moveCardTwo.transform.position = endTwo;
            moveCardTwo = null;
        }

        if (drawing)
        {

            c.SetBelongs(true);
            playerhand.AddCard(c);
            Card match = playerhand.IsMatchInHand();
            if(match != null)
            {
                yield return new WaitForSeconds(.5f);
                int tempValue = match.GetValue();
                startOne = match.transform;
                moveCardOne = match;
                endOne.position = matchPile.GetStackPoint(matchPile.GetPileSize());
                playerhand.RemoveCard(tempValue);
                StartCoroutine(transportCard(match, false, 1));
                StartCoroutine(commitToPile(match));
                Card leftOver = playerhand.RemoveCard(tempValue);
                moveCardTwo = leftOver;
                startTwo = leftOver.transform;
                endTwo = matchPile.GetStackPoint(matchPile.GetPileSize()+1);
                StartCoroutine(transportCard(leftOver, false, 2));
                StartCoroutine(commitToPile(leftOver));
            }
        }
    }
    */

    private IEnumerator transportCard(Card c,bool toPile)
    {
        c.showNumber = true;
        if (toPile)
        {
            AudioHelper.PlayClip2D(stackCardSound, 1f);
            c.SetBelongs(false);
            transporting.Add(c);
            c.start = c.transform.position;
            c.end = matchPile.GetStackPoint(matchPile.size);
            matchPile.size++;
            c.moving = true;
        }
        else
        {
            checking = false;
            transporting.Add(c);
            c.start = c.transform.position;
            c.end = playerhand.transform.position;
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
            playerhand.AddCard(c);
            c.SetBelongs(true);
            checking = true;
        }
        
    }

    private IEnumerator commitToPile(Card c)
    {
        yield return new WaitForSeconds(.5f);
        matchPile.AddCard(c);
        
    }

    private IEnumerator GoFishDelay(float f)
    {
        goFish.gameObject.SetActive(true);
        yield return new WaitForSeconds(f);
        goFish.gameObject.SetActive(false);
    }
}
