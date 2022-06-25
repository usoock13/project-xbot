using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public delegate void OnStay();
    public delegate void OnInactive();
    public delegate void OnActive();

    OnStay stay;
    OnInactive inActive;
    OnActive onActive;

    public void SetStay()
    {

    }

    public void SetInActive()
    {

    }

    public void SetOnActive()
    {

    }
}
