using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    static public GameObject instance = null;

    InputManager inputManager;

    bool scriptActiving = false;
    ScriptUI currentScript;

    public bool uiActiving { get; private set; } = false;
    InteractionUI currentUI;

    void Awake() {
        if(instance == null) {
            instance = this.gameObject;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
    void Start() {
        inputManager = InputManager.instance.GetComponent<InputManager>();
        DontDestroyOnLoad(this);
    }

    #region Script UI Area
    public void SetActiveScript(bool active) {
        scriptActiving = active;
        currentScript.SetActiveCanvas(active);
    }
    public void ChangeScript(ScriptUI ui) {
        currentScript = ui;
        currentScript.SetActiveCanvas(scriptActiving);
    }
    #endregion

    #region Interaction UI Area
    /* base method >> */
    public void SetActiveUI(bool active) {
        uiActiving = active;
        currentUI.SetActiveCanvas(active);
        if(active) {
            inputManager.ChangeInputState(InputState.interaction);
        } else {
            inputManager.ChangeInputState(InputState.battle);
        }
    }
    public void ChangeUI(InteractionUI ui) {
        currentUI.SetActiveCanvas(false);
        currentUI = ui;
        currentUI.SetActiveCanvas(uiActiving);
    }
    public void OpenUI(InteractionUI ui) {
        if(currentUI) currentUI.SetActiveCanvas(false);
        currentUI = ui;
        currentUI.SetActiveCanvas(true);
        uiActiving = true;
        inputManager.ChangeInputState(InputState.interaction);
    }
    /* << base method */
    public void CloseUIByEscape() {
        if(currentUI.closeByEscape) {
            SetActiveUI(false);
        }
    }
    #endregion
}
