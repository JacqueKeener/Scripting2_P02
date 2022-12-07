using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<int> cardValues = new List<int>();
    [SerializeField] List<Card> cardDeck = new List<Card>();


    private void Awake()
    {
        for(int i = 0; i < cardValues.Count; i++)
        {
            GameObject g = Instantiate(cardPrefab,transform);
            g.transform.Translate(0f, 0f, 0.01f * i);
            Card c = g.GetComponent<Card>();
            c.showNumber = false;
            c.SetBelongs(false);
            c.SetValue(cardValues[i]);
            cardDeck.Add(c);
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
            n--;
        }
    }

    public Card DealCard()
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
