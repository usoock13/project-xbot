using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : UI {
    public bool closeByEscape { get; private set; } = true;

    public void Close() {
        uiManager.SetActiveUI(false);
    }
}
