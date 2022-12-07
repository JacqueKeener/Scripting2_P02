using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int startingCardNumber = 5;
    [SerializeField] int numberOfPlayers = 2;
    [SerializeField] Hand enemyHand;
    [SerializeField] Hand playerHand;
    [SerializeField] Deck gameDeck;
    [SerializeField] AudioClip dealCardSound;

    private Card playerMoveCard;
    private Card enemyMoveCard;
    private int cardsDealt = 0;
    IEnumerator playerMoveTrack;
    IEnumerator enemyMoveTrack;
    

    bool activated = false;

    public override void Enter()
    {
        Debug.Log("Setup");

        if (!gameDeck.IsEmpty())
        {
            playerMoveTrack = transportCard(gameDeck.DealCard(), playerMoveTrack, false);
            StartCoroutine(playerMoveTrack);

            enemyMoveTrack = transportCard(gameDeck.DealCard(), enemyMoveTrack, true);
            StartCoroutine(enemyMoveTrack);
        }
        activated = false;
    }

    public override void Tick()
    {
        if(playerMoveTrack != null)
        {
            playerMoveCard.transform.Translate((playerHand.transform.position - playerMoveCard.transform.position).normalized * (Vector3.Distance(playerHand.transform.position, gameDeck.transform.position) * Time.deltaTime * 2f));
        }

        if (enemyMoveTrack != null)
        {
            enemyMoveCard.transform.Translate((enemyHand.transform.position - enemyMoveCard.transform.position).normalized * (Vector3.Distance(enemyHand.transform.position, gameDeck.transform.position) * Time.deltaTime * 2f));
        }
        /*
        if (activated == false)
        {
            activated = true;
            stateMachine.ChangeState<PlayerTurnCardGameState>();
        }*/
    }

    public override void Exit()
    {
        activated = false;
        Debug.Log("Setup Exit");
    }

    private IEnumerator transportCard(Card c,IEnumerator ie,bool isEnemy)
    {
        if (isEnemy)
        {
            enemyMoveCard = c;
        }
        else
        {
            playerMoveCard = c;
        }
        Debug.Log("Inside Transport");
        AudioHelper.PlayClip2D(dealCardSound, 1f);
        yield return new WaitForSeconds(.5f);
        if(isEnemy)
        {
            enemyHand.AddCard(c);
        }
        else
        {
            playerHand.AddCard(c);
            c.SetBelongs(true);
        }
        cardsDealt++;
        if (cardsDealt > 8)
        {
            stateMachine.ChangeState<PlayerTurnCardGameState>();
        }
        else
        {
            if(isEnemy)
            {
                enemyMoveTrack = transportCard(gameDeck.DealCard(), enemyMoveTrack, true);
                StartCoroutine(enemyMoveTrack);
            }
            else
            {
                playerMoveTrack = transportCard(gameDeck.DealCard(), playerMoveTrack, false);
                StartCoroutine(playerMoveTrack);
            }
        }
    }

}
