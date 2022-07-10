using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity {
    public Rigidbody playerRigidbody;
    protected StateMachine playerStateMachine;
    [SerializeField] protected Animator playerAnimator;
    [SerializeField] GameObject playerAvatar;
    Vector3 currentSpeed;

    float moveSpeed = 8f;
    float dodgePower = 35f;
    bool isDodge = false;

    protected State idleState = new State("idle");
    protected State moveState = new State("move");
    protected State dodgeState = new State("dodge");

    protected void Start() {
        playerStateMachine = GetComponent<StateMachine>();
        InitializeState();
    }
    void InitializeState() {
        moveState.OnActive += () => {
            playerAnimator.SetBool("Run", true);
        };
        moveState.OnStay += () => {
            // transform.Translate(currentSpeed * Time.deltaTime, Space.Self);
            // playerRigidbody.MovePosition(playerRigidbody.position + transform.TransformDirection(currentSpeed) * Time.deltaTime);
            playerRigidbody.velocity = transform.TransformDirection(currentSpeed);
            playerAvatar.transform.LookAt(transform.position + (Quaternion.AngleAxis(45, Vector3.up) * currentSpeed).normalized);
        };
        moveState.OnInactive += () => {
            playerAnimator.SetBool("Run", false);
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
        };
        dodgeState.OnActive += () =>{
            isDodge = true;
            Vector3 playerAngle = playerAvatar.transform.forward;
            StartCoroutine(DodgeCoroutine());
            playerAnimator.SetBool("Dodge", true);
            playerRigidbody.AddForce(playerAngle * dodgePower, ForceMode.Impulse);
        };
        dodgeState.OnInactive += () => {
            isDodge = false;
            playerAnimator.SetBool("Dodge", false);
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
        };
    }
    // Weapon
    virtual public void BasicAttack() {}
    virtual public void SpecialAttack() {}
    virtual public void UtilityAbility() {}
    // (Q)skill
    void PlayerAbility() {

    }
    public void Idle() {
        if(!isDodge) {
            this.currentSpeed = Vector3.zero;
            playerStateMachine.ChangeState(idleState);
        }
    }
    // Player Move
    public void Move(Vector3 direction)  {
        if(!isDodge) {
            this.currentSpeed = direction.normalized * moveSpeed;
            playerStateMachine.ChangeState(moveState);
        }
    }
    public void Dodge() {
        playerStateMachine.ChangeState(dodgeState);
    }
    IEnumerator DodgeCoroutine(){
         yield return new WaitForSeconds(.3f);
        playerStateMachine.ChangeState(idleState);
    }
}
