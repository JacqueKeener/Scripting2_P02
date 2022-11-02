using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private List<Card> cards = new List<Card>();

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    public void RemoveCard(int value)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].GetValue() == value)
            {
                cards.RemoveAt(i);
                break;
            }
        }
    }

    public int GetHandTotalValue()
    {
        int sum = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            sum += cards[i].GetValue();
        }
        return sum;
    }

    public Card IsMatchInHand()
    {
        Card ret = null;
        for (int i = 0; i < cards.Count - 1 ; i++)
        {
            for (int j = i + 1; j < cards.Count; j++)
            {
                if (cards[i].GetValue() == cards[j].GetValue())
                {
                    cards.RemoveAt(j);
                    cards.RemoveAt(i);
                    ret = cards[i];
                }
            }
        }
        return ret;
    }

}
