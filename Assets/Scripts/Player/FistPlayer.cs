using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayer : Player {
    protected State fistAttackState = new State("FistAttack");
    new void Start() {
        base.Start();
        InitializeState();
    }
    void InitializeState() {
        fistAttackState.OnActive += () => {
            canMove = false;
            playerAnimator.SetBool("Fist Attack",true);
            StartCoroutine(AttackCoroutine());
        };
        fistAttackState.OnInactive += () => {
            canMove = true;
            playerAnimator.SetBool("Fist Attack",false);
        };
    }
    public override void BasicAttack() {
        playerStateMachine.ChangeState(fistAttackState);
    }
    public override void SpecialAttack() {

    }
    public override void UtilityAbility() {

    }
    IEnumerator AttackCoroutine() {
        yield return new WaitForSeconds(0.8f);
        playerStateMachine.ChangeState(idleState);
    }
}
