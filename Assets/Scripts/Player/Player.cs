using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity {
    RaycastHit hit;
    [SerializeField] LayerMask layer;
    public Rigidbody playerRigidbody;
    protected StateMachine playerStateMachine;
    [SerializeField] protected Animator playerAnimator;
    [SerializeField] GameObject playerAvatar;
    Vector3 currentSpeed;
    float moveSpeed = 8f;
    float dodgePower = 35f;
    protected bool canMove = true;

    protected State idleState = new State("idle");
    protected State moveState = new State("move");
    protected State dodgeState = new State("dodge");
    protected State abilityState = new State("ability");
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
            canMove = false;
            Vector3 playerAngle = playerAvatar.transform.forward;
            StartCoroutine(DodgeCoroutine());
            playerAnimator.SetBool("Dodge", true);
            playerRigidbody.AddForce(playerAngle * dodgePower, ForceMode.Impulse);
        };
        dodgeState.OnInactive += () => {
            canMove = true;
            playerAnimator.SetBool("Dodge", false);
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
        };
        abilityState.OnActive += () => {

        };
        abilityState.OnInactive += () => {
            
        };
    }
    // Weapon
    virtual public void BasicAttack() {}
    virtual public void SpecialAttack() {}
    virtual public void UtilityAbility() {}
    // (Q)skill
    public void PlayerAbility() {
        playerStateMachine.ChangeState(abilityState);
    }
    public void Idle() {
        if(canMove) {
            this.currentSpeed = Vector3.zero;
            playerStateMachine.ChangeState(idleState);
        }
    }
    // Player Move
    public void Move(Vector3 direction)  {
        if(canMove) {
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
    void FrontCheck() {
        Vector3 look = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, look * 1, Color.red); 
        if(Physics.Raycast(transform.position + Vector3.up, look, out hit, 1, layer)) {
            Debug.Log("Raycast!");
        }
    }
    void Update() {
        FrontCheck();
    }
}
