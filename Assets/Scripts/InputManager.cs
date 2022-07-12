using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState {
    battle,
    interaction,
}

public class InputManager : MonoBehaviour {
    static public GameObject instance = null;

    [SerializeField] Player player;
    [SerializeField] GameObject inGameMenu;
    UIManager uIManager;

    public InputState inputState { get; private set; } = InputState.battle;

    void Awake() {
        if(instance == null) {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
        if(player == null) {
            player = GameObject.FindWithTag("Player").GetComponent<Player>();
        }
    }

    void Start() {
        uIManager = UIManager.instance.GetComponent<UIManager>();
        DontDestroyOnLoad(this);
    }

    void Update() {
        switch(inputState) {
            case InputState.battle:
                BattleInput();
                break;
            case InputState.interaction:
                InteractionInput();
                break;
        }
    }

    public void ChangeInputState(InputState nextState) {
        if(inputState != nextState) {
            inputState = nextState;
        }
    }
    void BattleInput() {
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(inputAxis != Vector2.zero) {
            Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);
            player.Move(moveDirection);
        } else {
            player.Idle();
        }
        if(Input.GetButtonDown("Dodge")) {
            player.Dodge();
        }
        if(Input.GetMouseButtonDown(0)) {
            player.BasicAttack();
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            InteractionUI target = inGameMenu.GetComponent<InteractionUI>();
            if(target) {
                uIManager.OpenUI(target);
            }
        }
    }
    void InteractionInput() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            uIManager.CloseUIByEscape();
        }
    }
}
