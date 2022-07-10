using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveableEnemy : Enemy {
    State idleState = new State("Idle");
    State chaseState = new State("Chase");
    State hitState = new State("Hit");

    Animator enemyAnimator;

    Coroutine chaseCoroutine;

    protected override void Start() {
        base.Start();
        if(enemyAnimator == null) enemyAnimator = ownAvatar.GetComponent<Animator>();

        var detectEventCaster = detectRange.GetComponent<PhysicsEventBroadcaster>();
        if(detectEventCaster) {
            detectEventCaster.triggerStayEvent += OnDetectTarget;
            detectEventCaster.triggerExitEvent += OnExitTarget;
        }
        var attackEventCaster = attackRange.GetComponent<PhysicsEventBroadcaster>();
        if(attackEventCaster) {
            attackEventCaster.triggerEnterEvent += OnReachTarget;
        }
    }
    protected virtual void InitializeState() {
        chaseState.OnActive += () => {
            enemyAnimator.SetBool("Chase", true);
            chaseCoroutine = StartCoroutine(ChaseTarget());
        };
        chaseState.OnInactive += () => {
            enemyAnimator.SetBool("Chase", false);
            ClearTarget();
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
    public virtual void OnExitTarget(Collider other) {
        if(currentTarget == other.gameObject) {
            enemyStateMachine.ChangeState(idleState);
        }
    }
    public virtual void OnReachTarget(Collider other) {
        print("I wanna hit you.");
    }
}
