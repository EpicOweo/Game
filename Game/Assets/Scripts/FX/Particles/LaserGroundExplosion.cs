using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserGroundExplosion : MonoBehaviour
{
    public Laser parent;
    public ParticleSystem particles;

    void Awake() {
        parent.onIntersectTerrain.AddListener((bool initial, Collider2D col) => {
            RaycastHit2D hit;

            Vector3 dir = parent.transform.rotation * Vector3.right;

            if (
                hit = Physics2D.Raycast(
                    parent.pivot.transform.position,
                    dir,
                    parent.GetComponent<BoxCollider2D>().size.x*2, // just * 2 to make it big
                    LayerMask.GetMask("Terrain")
                )
            ) {

                float newLength = hit.distance;

                parent.SetLaserLength(newLength);
                transform.position = hit.point;
                particles.transform.position = hit.point;
                gameObject.SetActive(true);
                
            } else {
                gameObject.SetActive(false);
            }
        });
    }
}
