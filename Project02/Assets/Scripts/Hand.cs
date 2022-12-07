using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    public bool isPlayer = false;

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    public Card RemoveCard(int value)
    {
        Card ret = null;
        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].GetValue() == value)
            {
                ret = cards[i];
                cards.RemoveAt(i);
                break;
            }
        }
        return ret;
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
                    ret = cards[i];
                    //cards.RemoveAt(j);
                    //cards.RemoveAt(i);
                }
            }
        }
        return ret;
    }

    private void Update()
    {

        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cards.Count - 1; j++)
            {
                if (cards[j].GetValue() > cards[j + 1].GetValue())
                {
                    Card temp = cards[j + 1];
                    cards[j + 1] = cards[j];
                    cards[j] = temp;
                }
            }
        }

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = new Vector3(transform.position.x + ((0.5f * i) - (.075f * cards.Count)), transform.position.y, transform.position.z + (0.01f * i));
            if (isPlayer)
            {
                cards[i].showNumber = true;
            }
        }
    }

    public Vector3 GetCardPosition(int i)
    {
        return cards[i].transform.position;
    }

    public int GetHandSize()
    {
        return cards.Count;
    }


    public int GetValue(int i)
    {
        return cards[i].GetValue();
    }

    public bool IsInHand(int value)
    {
        bool ret = false;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetValue() == value)
            {
                ret = true;
            }
        }
        return ret;
    }
}
