using UnityEngine;

public class Entity : MonoBehaviour {
    public Health health;
    public Rigidbody2D rb; 

    protected virtual void Start() {
        health.onKill.AddListener(() => Destroy(gameObject));
    }
}