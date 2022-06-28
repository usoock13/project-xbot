using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : UI {
    public void Close() {
        uiManager.SetActiveUI(false);
    }
}
