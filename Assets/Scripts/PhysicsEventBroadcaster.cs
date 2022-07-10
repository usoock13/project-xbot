using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEventBroadcaster : MonoBehaviour {
    public delegate void TriggerEvent(Collider other);

    public TriggerEvent triggerEnterEvent;
    public TriggerEvent triggerStayEvent;
    public TriggerEvent triggerExitEvent;

    void OnTriggerEnter(Collider other) {
        if(triggerEnterEvent != null) triggerEnterEvent(other);
    }
    void OnTriggerStay(Collider other) {
        if(triggerStayEvent != null) triggerStayEvent(other);
    }
    void OnTriggerExit(Collider other) {
        if(triggerExitEvent != null) triggerExitEvent(other);
    }
}
