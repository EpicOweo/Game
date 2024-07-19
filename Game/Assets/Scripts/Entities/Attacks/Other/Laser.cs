using System;
using UnityEngine;
using UnityEngine.Events;

public class Laser : Entity {


    public bool ignoreTerrain = true;

    public int damage = 10;
    public bool instakill = false;
    public SpriteRenderer nineSlicedRenderer; 
    public BoxCollider2D col;
    public Transform pivot;

    [NonSerialized] public UnityEvent<bool, Collider2D> onIntersectTerrain = new(); // bool is for initial collision

    void Awake() {
        health.invulnerable = true;
        col = GetComponent<BoxCollider2D>();
        pivot.transform.localPosition = new Vector3(-nineSlicedRenderer.size.x/2, 0, 0);
    }

    public void SetLaserLength(float length) {
        float dx = (length - nineSlicedRenderer.size.x) / 2;

        nineSlicedRenderer.size = new(length, nineSlicedRenderer.size.y);
        col.size = new(length, col.size.y);

        //nineSlicedRenderer.transform.localPosition += new Vector3(dx, 0, 0);
        col.transform.localPosition += new Vector3(0, dx, 0);
        pivot.transform.localPosition = new Vector3(-length/2, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            if(instakill) Player.instance.health.Kill();
            else Player.instance.health.Damage(damage);
        }
        else if(!ignoreTerrain
            && collider.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
            
            onIntersectTerrain.Invoke(true, collider);
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.CompareTag("PlayerCollider")) {
            Player.instance.health.Damage(damage);
        } else if(!ignoreTerrain
            && collider.gameObject.layer == LayerMask.NameToLayer("Terrain")) {

            onIntersectTerrain.Invoke(false, collider);
        }
    }

}