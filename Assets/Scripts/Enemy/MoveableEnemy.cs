using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableEnemy : Enemy {
    protected State idleState = new State("Idle");
    protected State chaseState = new State("Chase");
    protected State hitState = new State("Hit");

    Coroutine chaseCoroutine;

    public float moveSpeed { get; private set; } = 5f;
    public float SetMoveSpeed {
        set{
            moveSpeed = value;
            enemyNavAgent.speed = moveSpeed;
        }
    }

    protected override void Start() {
        base.Start();
        enemyNavAgent.speed = moveSpeed;
        if(enemyAnimator == null) enemyAnimator = ownAvatar.GetComponent<Animator>();
        if(enemyRigidbody == null) enemyRigidbody = GetComponent<Rigidbody>();

        var detectEventCaster = detectRange.GetComponent<PhysicsEventBroadcaster>(); // Initialize object that check target in range of detect
        if(detectEventCaster) {
            detectEventCaster.triggerStayEvent += OnDetectTarget;
        }
        var attackEventCaster = attackRange.GetComponent<PhysicsEventBroadcaster>(); // Initialize object that check target in range of attack
        if(attackEventCaster) {
            attackEventCaster.triggerEnterEvent += OnReachTarget;
            attackEventCaster.triggerStayEvent += OnStayTargetInReach;
        }
    }
    protected virtual void InitializeState() {
        idleState.OnStay += () => {
            if(currentTarget != null)
                enemyStateMachine.ChangeState(chaseState);
        };
        chaseState.OnActive += () => {
            enemyAnimator.SetBool("Chase", true);
            enemyNavAgent.isStopped = false;
            chaseCoroutine = StartCoroutine(ChaseTarget());
        };
        chaseState.OnInactive += () => {
            enemyAnimator.SetBool("Chase", false);
            enemyNavAgent.isStopped = true;
            StopCoroutine(chaseCoroutine);
        };
    }
    public virtual IEnumerator ChaseTarget() {
        while(currentTarget != null) {
            Vector3 targetPosition = currentTarget.transform.position;
            Vector3 heightlessTargetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            enemyNavAgent.SetDestination(targetPosition);
            ownAvatar.transform.LookAt(heightlessTargetPosition);
            yield return new WaitForSeconds(.1f);
        }
    }
    public virtual void OnDetectTarget(Collider other) {
        if(other.tag == "Player") {
            SetTarget(other.gameObject);
            enemyStateMachine.ChangeState(chaseState);
        }
    }
    public virtual void OnReachTarget(Collider other) {}
    public virtual void OnStayTargetInReach(Collider other) {}
}
