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
            Debug.Log("주먹쫌날려봐 이새끼야");
            playerAnimator.SetBool("Fist Attack",true);
        };
        fistAttackState.OnInactive += () => {
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
}
