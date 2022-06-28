using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
    string stateName;

    public State(string name) {
        stateName = name;
    }
    public static bool operator == (State a, State b) {
        return Object.Equals(a, b);
    }
    public static bool operator != (State a, State b) {
        return !Object.Equals(a, b);
    }
    public override bool Equals(object other){
        if(other == null)
            return false;

        if(GetType() == other.GetType()) {
            return Equals((State)other);
        }
        return false;
    }
    public bool Equals(State other){
        return stateName == other.stateName;
    }
    public override int GetHashCode() {
        return int.Parse(stateName);
    }
    public delegate void StayDelegate();
    public delegate void ActiveDelegate();
    public delegate void InactiveDelegate();

    public StayDelegate OnStay;
    public ActiveDelegate OnActive;
    public InactiveDelegate OnInactive;
}
