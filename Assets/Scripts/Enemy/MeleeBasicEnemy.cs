using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeBasicEnemy : MoveableEnemy {
    State beforeAttackState = new State("Before Attack");
    State attackingState = new State("Attacking");

    protected override void Start() {
        base.Start();
        InitializeState();
    }
    protected override void InitializeState() {
        base.InitializeState();
    }
    public override void OnDetectTarget(Collider other) {
        base.OnDetectTarget(other);
    }
    public override void OnExitTarget(Collider other) {
        base.OnExitTarget(other);
    }
    public override void OnReachTarget(Collider other) {
        base.OnReachTarget(other);
        print("and kill you");
    }
}
