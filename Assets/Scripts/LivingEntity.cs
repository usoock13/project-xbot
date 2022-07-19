using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : Entity {
    [SerializeField] protected bool isAlive;
    [SerializeField] protected float healthPoint;

    public void OnDie() {
        isAlive = true;
    }
}
