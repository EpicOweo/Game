using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHazard : MonoBehaviour
{
    public int damage;

    void OnTriggerStay2D(Collider2D collider) {
        if(collider.gameObject.Equals(Player.instance.gameObject)) {
            //Player.instance.health.Damage(damage);
            Player.instance.health.Kill();
        }
    }
}
