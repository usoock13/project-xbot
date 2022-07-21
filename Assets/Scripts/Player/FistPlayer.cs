using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistPlayer : Player {
    protected State firstAttackState = new State("firstAttack");
    protected State secondAttackState = new State("secondAttack");
    protected State thirdAttackState = new State("thirdAttack");
    public float coolTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    private float lastClickTime = 0;
    private float maxComboDelay = 1f;
    new void Start() {
        base.Start();
        InitializeState();
    }
    void InitializeState() {
        firstAttackState.OnActive += () => {
            canMove = false;
            playerAnimator.SetBool("FistAttack01",true);
        };
        firstAttackState.OnInactive += () => {
            canMove = true;
            playerAnimator.SetBool("FistAttack01",false);
        };
        secondAttackState.OnActive += () => {
            canMove = false;
            playerAnimator.SetBool("FistAttack02",true);
        };
        secondAttackState.OnInactive += () => {
            canMove = true;
            playerAnimator.SetBool("FistAttack02",false);
        };
        thirdAttackState.OnActive += () => {
            canMove = false;
            playerAnimator.SetBool("FistAttack03",true);
        };
        thirdAttackState.OnInactive += () => {
            canMove = true;
            playerAnimator.SetBool("FistAttack03",false);
            noOfClicks = 0;
        };
    }
    public override void BasicAttack() {
        lastClickTime = Time.time;
        noOfClicks++;
        if(noOfClicks == 1) {
            playerStateMachine.ChangeState(firstAttackState);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if(noOfClicks >= 2 && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("FistAttack01")) {
            playerStateMachine.ChangeState(secondAttackState);
        }
        if(noOfClicks >= 3 && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("FistAttack02")) {
            playerStateMachine.ChangeState(thirdAttackState);
        }
    }
    public override void SpecialAttack() {

    }
    public override void UtilityAbility() {

    }
    IEnumerator AttackDelay() {
        yield return new WaitForSeconds(0.8f);
        playerStateMachine.ChangeState(idleState);
    }
}
