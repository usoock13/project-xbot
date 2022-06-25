using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity {
    StateMachine playerStateMachine;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject playerAvatar;

    Vector3 currentSpeed;
    float moveSpeed = 11f;

    State idleState = new State("idle");
    State moveState = new State("move");

    void Start() {
        playerStateMachine = GetComponent<StateMachine>();
        moveState.OnActive += () => {
            playerAnimator.SetBool("Run", true);
        };
        moveState.OnStay += () => {
            transform.Translate(currentSpeed * Time.deltaTime, Space.Self);
            playerAvatar.transform.LookAt(transform.position + (Quaternion.AngleAxis(45, Vector3.up) * currentSpeed).normalized);
        };
        moveState.OnInactive += () => {
            playerAnimator.SetBool("Run", false);
        };
    }
    // Weapon
    void BasicAttack() {

    }
    void SpecialAttack() {

    }
    void UtilityAbility() {

    }
    // Q 스킬 (개인적으로 가지는 능력)
    void PlayerAbility() {

    }
    public void Idle() {
        this.currentSpeed = Vector3.zero;
        playerStateMachine.ChangeState(idleState);
    }
    // Player Move
    public void Move(Vector3 direction)  {
        this.currentSpeed = direction * moveSpeed;
        playerStateMachine.ChangeState(moveState);
    }
    void Dodge() {

    }
    void Update() {

    }
}
