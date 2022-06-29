using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    Entity origin;
    Vector3 direction;
    float harmDegree;
    float hittingDelay;

    void Start() {
        origin = GetComponent<Entity>();
    }
}
