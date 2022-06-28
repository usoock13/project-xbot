using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity {
    public Rigidbody playerRigidbody;

    StateMachine playerStateMachine;
    [SerializeField] Animator playerAnimator;
    [SerializeField] GameObject playerAvatar;
    Vector3 currentSpeed;

    float moveSpeed = 5f;
    bool canMove = true;
    bool isDodge = false;

    State idleState = new State("idle");
    State moveState = new State("move");
    State dodgeState = new State("dodge");

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
        dodgeState.OnActive += () =>{
            Vector3 playerAngle = playerAvatar.transform.forward;
            StartCoroutine(DodgeCoroutine());
            playerAnimator.SetBool("Dodge",true);
            canMove = false;
            playerRigidbody.AddForce(playerAngle * moveSpeed ,ForceMode.Impulse);
        };
        dodgeState.OnInactive += () =>{
            playerAnimator.SetBool("Dodge",false);
            canMove = true;
        };
    }
    // Weapon
    void BasicAttack() {

    }
    void SpecialAttack() {

    }
    void UtilityAbility() {

    }
    // (Q)skill
    void PlayerAbility() {

    }
    public void Idle() {
        if(playerStateMachine.currentState != dodgeState){
            this.currentSpeed = Vector3.zero;
            playerStateMachine.ChangeState(idleState);
        }
        
    }
    // Player Move
    public void Move(Vector3 direction)  {
        this.currentSpeed = direction * moveSpeed;
        playerStateMachine.ChangeState(moveState);
    }
    public void Dodge() {
        playerStateMachine.ChangeState(dodgeState);
    }

    void GroundCheck(){

    }

    IEnumerator DodgeCoroutine(){
         yield return new WaitForSeconds(0.3f);
        playerStateMachine.ChangeState(idleState);
    }
}
