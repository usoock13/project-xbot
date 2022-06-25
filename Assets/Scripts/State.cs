using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {
    string stateName;

    public State(string name) {
        stateName = name;
    }

    public delegate void StayDelegate();
    public delegate void ActiveDelegate();
    public delegate void InactiveDelegate();

    public StayDelegate OnStay;
    public ActiveDelegate OnActive;
    public InactiveDelegate OnInactive;
}
