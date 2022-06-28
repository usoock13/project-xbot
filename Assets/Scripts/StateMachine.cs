using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    public State currentState {get; private set;}
    
    public void ChangeState(State nextState) {
        if(currentState == null) {
            currentState = nextState; // currentState >> (A > B)
            if(currentState.OnActive != null) currentState.OnActive(); // B.OnActive()
        } else {
            if(currentState != nextState) {
                if(currentState.OnInactive != null) currentState.OnInactive(); // A.OnActive()
                currentState = nextState; // currentState >> (A > B)
                if(currentState.OnActive != null) currentState.OnActive(); // B.OnActive()
            }
        }
    }
    void Update() {
        if(currentState != null && currentState.OnStay != null) currentState.OnStay();
    }
}
