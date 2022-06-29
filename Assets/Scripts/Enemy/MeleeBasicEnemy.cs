using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeBasicEnemy : Enemy {
    public override void ChaseTarget() {
        if(currentTarget != null) {
            enemyNavAgent.SetDestination(currentTarget.transform.position);
        }
    }
}
