using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalProjectile : Projectile {
    [Header("Base")]
    public float flySpeed;
    public float destroyTime = 10f;
    private Vector3 moveDirection;

    [Header("Deceleration relate")]
    public bool hasDeceleration = false;
    public float decelerationDegree = 1f;
    
    public bool isRotateToGround;

    protected override void Start() {
        base.Start();
        moveDirection = transform.forward;
    }
    protected override void Update() {
        base.Update();
        Fly();
        if(lifeTime >= destroyTime) {
            Disappear();
        }
    }
    protected override void Fly() {
        Vector3 moveVector = flySpeed * moveDirection;
        transform.Translate(moveVector * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter(Collision other) {
        OnHitOther(other);
    }
    protected override void Disappear() {
        /* for debug << */
        lifeTime = 0;
        transform.position = new Vector3(5, 2, -5);
        /* << for debug */
        base.Start();
    }
}
