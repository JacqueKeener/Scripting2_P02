using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    public int size;
    public int score;

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    // Start is called before the first frame update
    void Start()
    {
        size = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if(i % 2 == 0)
            {
                cards[i].transform.position = new Vector3(transform.position.x + (0.7f * (i / 2)), transform.position.y, transform.position.z + 0.01f);
            }
            else
            {
                cards[i].transform.position = new Vector3(transform.position.x + (0.7f * (i / 2)), transform.position.y + 0.15f, transform.position.z);
            }
        }
    }

    public Vector3 GetStackPoint(int i)
    {
        if(i % 2 == 0)
        {
            return new Vector3(transform.position.x + (0.7f * (i / 2)), transform.position.y, transform.position.z);
        }
        else
        {
            return new Vector3(transform.position.x + (0.7f * (i / 2)), transform.position.y + 0.15f, transform.position.z);
        }
    }

}
