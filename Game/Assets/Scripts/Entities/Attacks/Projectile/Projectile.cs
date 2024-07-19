using System;
using UnityEngine;

public class Projectile : Entity {
    public Vector2 initialDirection;
    public float initialSpeed;
    public float acceleration;
    public float angularVelDeg;
    public float duration = 5;
    public int damage;
    public bool ignoreTerrain = false;


    public Vector2 rotationPivotPoint;
    public bool rotatingAboutPivot = false;

    public bool lastIndefinitely = false;

    public bool pooled = false;

    [NonSerialized] public float time = 0;

    protected override void Start() {
        health.onKill.AddListener(() => {
            if(pooled) {
                gameObject.SetActive(false);
                health.Reset();
            } 
            else Destroy(gameObject);
        });
    }

    void Update() {
        time += Time.deltaTime;
        if(!rotatingAboutPivot) {
            rb.velocity = GetDirectionAtTime(time).normalized * GetSpeedAtTime(time);
        } else {
            transform.RotateAround(rotationPivotPoint, Vector3.forward, angularVelDeg * Time.deltaTime);
        }

        if(!lastIndefinitely && time > duration) {
            if(pooled) gameObject.SetActive(false);
            else Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            Player.instance.health.Damage(damage);
            health.Damage(1);
        } else if(!ignoreTerrain
            && collider.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
                
            health.Kill();
        }
    }

    public virtual Vector2 GetDirectionAtTime(float t) {
        return Quaternion.AngleAxis(angularVelDeg * t, Vector3.forward) * initialDirection;
    }

    public virtual float GetSpeedAtTime(float t) {
        return initialSpeed + acceleration * t;
    }
}