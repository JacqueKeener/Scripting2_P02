using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    
    [SerializeField] List<Sprite> cardFronts = new List<Sprite>();
    [SerializeField] Sprite cardBack;
    [SerializeField] List<int> cardValues = new List<int>();
    [SerializeField] List<Card> cardDeck = new List<Card>();


    private void Awake()
    {
        for(int i = 0; i < cardValues.Count; i++)
        {
            if(i < cardFronts.Count)
            {
                cardDeck.Add(new Card(cardFronts[i], cardBack, cardValues[i]));
            }
            else
            {
                cardDeck.Add(new Card(cardBack, cardBack, cardValues[i]));
            }
        }
        ShuffleDeck();
    }

    private void ShuffleDeck()
    {
        int n = cardDeck.Count - 1;
        while(n > 0)
        {
            int r = Random.Range(0, cardDeck.Count);
            Card c = cardDeck[n];
            cardDeck[n] = cardDeck[r];
            cardDeck[r] = c;
        }
    }

    public Card DrawCard()
    {
        Card ret = cardDeck[0];
        cardDeck.RemoveAt(0);
        return ret;
    }

    public bool IsEmpty()
    {
        return cardDeck.Count == 0;
    }
}
