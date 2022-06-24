using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour {
    static protected UIManager uiManager;
    
    [SerializeField]
    private Canvas ownCanvas;

    void Start() {
        uiManager = UIManager.instance.GetComponent<UIManager>();
    }

    public void SetActiveCanvas(bool active) {
        ownCanvas.gameObject.SetActive(active);
    }
}
