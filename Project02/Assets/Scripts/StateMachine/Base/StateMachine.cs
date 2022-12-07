using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState => currentState;
    protected bool inTransition { get; private set; }
    State currentState;
    protected State previousState;


    public int leftScore = 0;
    public int rightScore = 0;
    public bool deckHasCards = true;

    public void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if(targetState == null)
        {
            Debug.LogWarning("Cannot change state as it does not exist on the State Machine Object");
            return;
        }
        InitiateStateChange(targetState);
    }
    
    public void RevertState()
    {
        if(previousState != null)
        {
            InitiateStateChange(previousState);
        }
    }

    void InitiateStateChange(State targetState)
    {
        if(currentState != targetState && !inTransition)
        {
            Transition(targetState);
        }
    }

    void Transition(State newState)
    {
        inTransition = true;
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
        inTransition = false;
    }

    private void Update()
    {
        
        if(CurrentState != null && !inTransition)
        {
            currentState.Tick();
        }
    }

}
