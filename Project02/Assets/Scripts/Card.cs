using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private int cardValue;
    private bool belongsToPlayer;
    public Vector3 start;
    public Vector3 end;
    public bool moving;
    public Text num;
    public bool showNumber;

    private void Start()
    {
        start = new Vector3(0f, 0f, 0f);
        end = new Vector3(0f, 0f, 0f);
        moving = false;
    }

    public int GetValue()
    {
        return cardValue;
    }

    public void SetValue(int v)
    {
        cardValue = v;
    }

    public void SetBelongs(bool b)
    {
        belongsToPlayer = b;
    }

    public bool GetBelongs()
    {
        return belongsToPlayer;
    }

    private void Update()
    {
        if (showNumber) 
        {
            num.text = cardValue.ToString();
        }
        else
        {
            num.text = "";
        }
        
        if (moving)
        {
            transform.Translate((end - start).normalized * (Vector3.Distance(end, start) * Time.deltaTime * 2f));
        }
        
    }

}
