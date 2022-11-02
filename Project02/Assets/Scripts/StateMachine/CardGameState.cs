using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CardGameSM))]
public class CardGameState : State
{
        protected CardGameSM stateMachine { get; private set; }

        void Awake()
        {
        stateMachine = GetComponent<CardGameSM>();
        }
}

