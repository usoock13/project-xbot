using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    static public GameObject instance = null;

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
    public void SetActiveUI(bool active) {
        uiActiving = active;
        currentUI.SetActiveCanvas(active);
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
    }
    #endregion
}
