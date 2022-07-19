using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcProjectile : Projectile
{
    public Vector3 targetPoint;
    public float flightTime = 2f;
    public enum FlyBase { Time, Speed }
    public FlyBase flyBase = FlyBase.Time;
    Vector3 startPoint;
    /* it gonna disappear */
    public Vector3 tPoint = new Vector3(0, 0, -6);
    /*  */

    protected override void Start() {
        base.Start();
        startPoint = transform.position;
        /* test code >> */
        targetPoint = GameObject.FindWithTag("Player").transform.position;
        /* << test code */
    }
    protected override void Update() {
        base.Update();
        Fly();
    }
    protected override void Fly() {
        if(flyBase == FlyBase.Time) {
            float interpolation = 0;
            float minHeight = Mathf.Min(startPoint.y, targetPoint.y);
            float maxHeight = Mathf.Max(startPoint.y, targetPoint.y) + 5 * (flightTime + Mathf.Pow(flightTime, 2)/10);
            bool startBottom = startPoint.y < targetPoint.y;
            if(startPoint.y != targetPoint.y) {
                interpolation = startBottom
                                ? Mathf.Asin((targetPoint.y - minHeight) / (maxHeight - minHeight))
                                : Mathf.Asin((startPoint.y - minHeight) / (maxHeight - minHeight));
            }
            
            float yy = Mathf.LerpUnclamped(
                minHeight,
                maxHeight,
                startBottom
                    ? Mathf.Sin(((lifeTime / flightTime) * ((Mathf.PI-interpolation) / (Mathf.PI)) * Mathf.PI))
                    : Mathf.Sin((interpolation + (lifeTime / flightTime) * ((Mathf.PI-interpolation) / (Mathf.PI)) * Mathf.PI))
            );
            
            print(minHeight);
            print(maxHeight);
            print(interpolation);

            float xx = Mathf.Lerp(startPoint.x, targetPoint.x, lifeTime / flightTime);
            float zz = Mathf.Lerp(startPoint.z, targetPoint.z, lifeTime / flightTime);
            transform.position = new Vector3(xx, yy, zz);

            if(lifeTime > flightTime) {
                OnLand();
            }
        }
    }
    void OnLand() {
        /* test code >> */
        GameObject nextT = this.gameObject;
        nextT.name = "projectile_by_the_way";
        Instantiate(nextT, tPoint, Quaternion.identity);
        Destroy(this.gameObject);
        /* << test code */
    }
}
