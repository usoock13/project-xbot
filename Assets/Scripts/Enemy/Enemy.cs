using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : LivingEntity {
    protected NavMeshAgent enemyNavAgent;
    protected GameObject playerObject;
    protected GameObject currentTarget;

    public void Start() {
        enemyNavAgent = GetComponent<NavMeshAgent>();
        enemyNavAgent.updateRotation = false;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        ChaseTarget();
    }
    public void SetTarget(GameObject target) {
        currentTarget = target;
    }
    public virtual void ChaseTarget() {
        Invoke("ChaseTarget", .3f);
    }
}
