using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeBasicEnemy : MoveableEnemy {
    State beforeAttackState = new State("Before Attack");
    State attackingState = new State("Attacking");
    State afterAttackState = new State("After Attack");

    protected override void Start() {
        base.Start();
        InitializeState();
    }
    protected override void InitializeState() {
        base.InitializeState();
        beforeAttackState.OnActive += () => {
            enemyAnimator.SetBool("Before Attack", true);
            enemyRigidbody.velocity = Vector3.zero;
        };
        beforeAttackState.OnInactive += () => {
            enemyAnimator.SetBool("Before Attack", false);
        };
        attackingState.OnActive += () => {
            enemyAnimator.SetBool("Attacking", true);
        };
        attackingState.OnInactive += () => {
            enemyAnimator.SetBool("Attacking", false);
            enemyRigidbody.velocity = Vector3.zero;
        };
        afterAttackState.OnActive += () => {
            enemyAnimator.SetBool("After Attack", true);
        };
        afterAttackState.OnInactive += () => {
            enemyAnimator.SetBool("After Attack", false);
        };
        hitState.OnActive += () => {
            enemyAnimator.SetBool("Hit", true);
            enemyRigidbody.velocity = Vector3.zero;
        };
        hitState.OnInactive += () => {
            enemyAnimator.SetBool("Hit", false);
        };
    }
    public void Update() {
        print(enemyStateMachine.currentState);
    }
    public override void OnDetectTarget(Collider other) {
        if(enemyStateMachine.currentState == attackingState
        || enemyStateMachine.currentState == beforeAttackState
        || enemyStateMachine.currentState == afterAttackState)
            return;
        base.OnDetectTarget(other);
    }
    public override void OnStayTargetInReach(Collider other) {
        if(currentTarget == null) return;
        if(other.tag == "Player") {
            if(enemyStateMachine.currentState == beforeAttackState
            || enemyStateMachine.currentState == attackingState
            || enemyStateMachine.currentState == afterAttackState)
                return;

            base.OnStayTargetInReach(other);
            StartCoroutine(AttackCoroutine());
        }
    }
    private IEnumerator AttackCoroutine() {
        enemyStateMachine.ChangeState(beforeAttackState);
        Vector3 direction = (currentTarget.transform.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        ownAvatar.transform.LookAt(currentTarget.transform.position);
        yield return new WaitForSeconds(.7f);
        float assaultSpeed = (moveSpeed * 1.5f);
        float moveDistance = 0;
        enemyStateMachine.ChangeState(attackingState);
        enemyRigidbody.velocity = Vector3.zero;
        enemyRigidbody.velocity = direction * assaultSpeed;
        while(enemyStateMachine.currentState == attackingState && moveDistance < 10f) {
            moveDistance += assaultSpeed * Time.deltaTime;
            yield return null;
        }
        enemyStateMachine.ChangeState(afterAttackState);
        yield return new WaitForSeconds(1.5f);
        enemyStateMachine.ChangeState(idleState);
    }
}
