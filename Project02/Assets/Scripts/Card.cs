using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;

    private int cardValue;

    public Card(Sprite front, Sprite back, int value)
    {
        cardFront = front;
        cardBack = back;
        cardValue = value;
    }

    public int GetValue()
    {
        return cardValue;
    }

}
