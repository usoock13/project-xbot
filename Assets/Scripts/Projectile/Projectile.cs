using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void voidEvent();
public delegate void collisionEvent(Collision other);
public abstract class Projectile : MonoBehaviour {
    protected float lifeTime = 0;

    public voidEvent OnDisappear;
    public collisionEvent OnHitOther;
    protected Collider collisionArea;

    protected abstract void Fly();
    protected virtual void Update() {
        lifeTime += Time.deltaTime;
    }
    protected virtual void Start() {
        collisionArea = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision other) {
        OnHitOther(other);
    }
    protected virtual void Disappear() {
        if(OnDisappear != null)
            OnDisappear();
    }
}
