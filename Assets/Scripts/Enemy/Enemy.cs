using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// melee, throwing, totem, huge, mage
public abstract class Enemy : LivingEntity {
    protected GameObject playerObject;

    protected StateMachine enemyStateMachine;

    protected NavMeshAgent enemyNavAgent;
    protected GameObject currentTarget;

    protected Animator enemyAnimator;
    protected Rigidbody enemyRigidbody;

    protected float pathUpdateInterval = .05f;

    [SerializeField] protected GameObject detectRange;
    [SerializeField] protected GameObject attackRange;
    [SerializeField] protected GameObject ownAvatar;

    protected virtual void Start() {
        enemyNavAgent = GetComponent<NavMeshAgent>();
        enemyNavAgent.updateRotation = false;
        
        enemyStateMachine = GetComponent<StateMachine>();

        if(ownAvatar == null)
            try { ownAvatar = transform.Find("Avatar").gameObject; }
            catch { Debug.LogError("Can not find Enemy own Avatar"); }

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }
    public void SetTarget(GameObject target) {
        currentTarget = target;
    }
    public void ClearTarget() {
        currentTarget = null;
    }
    public void ClearTarget(GameObject target) {
        if(currentTarget == target) {
            currentTarget = null;
        }
    }
}
