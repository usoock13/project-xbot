using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThrowingEnemy : MoveableEnemy {
    State attackState = new State("Attack");

    [SerializeField] float attackSpeed = 1f;

    protected override void Start() {
        base.Start();
        InitializeState();
    }
    protected override void InitializeState() {
        base.InitializeState();
        attackState.OnActive += () => {
            enemyAnimator.SetBool("Attacking", true);
        };
        attackState.OnInactive += () => {
            enemyAnimator.SetBool("Attacking", false);
            enemyRigidbody.velocity = Vector3.zero;
        };
        hitState.OnActive += () => {
            enemyAnimator.SetBool("Hit", true);
            enemyRigidbody.velocity = Vector3.zero;
        };
        hitState.OnInactive += () => {
            enemyAnimator.SetBool("Hit", false);
        };
    }
    public override void OnDetectTarget(Collider other) {
        if(enemyStateMachine.currentState == attackState)
            return;
        base.OnDetectTarget(other);
    }
    public override void OnStayTargetInReach(Collider other) {
        if(currentTarget == null) return;
        if(other.tag == "Player") {
            if(enemyStateMachine.currentState == attackState)
                return;

            base.OnStayTargetInReach(other);
            StartCoroutine(AttackCoroutine());
        }
    }
    private IEnumerator AttackCoroutine() {
        enemyStateMachine.ChangeState(attackState);
        Vector3 direction = (currentTarget.transform.position - transform.position);
        direction.y = 0;
        direction = direction.normalized;
        ownAvatar.transform.LookAt(currentTarget.transform.position);
        yield return new WaitForSeconds(1 / attackSpeed);
        
        /* Throwing stuff code in here*/
    }
}
