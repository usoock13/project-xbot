using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : Entity {
    public bool isAlive;
    public float heathPoint;

    public void OnDie() {
        isAlive = true;
    }
}
