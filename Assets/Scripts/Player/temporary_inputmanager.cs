using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporary_inputmanager : MonoBehaviour {
    [SerializeField]
    Player player;

    void Update() {
        CheckInput();
    }

    void CheckInput() {
        Vector3 axis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        axis = axis.normalized;
        if(axis != Vector3.zero) {
            player.Move(axis);
        } else {
            player.Idle();
        }
    }
}
