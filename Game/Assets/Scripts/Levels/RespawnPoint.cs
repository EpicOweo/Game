using Unity.VisualScripting;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    public static RespawnPoint activeRespawnPoint;

    public bool initialRespawnPoint = false;

    void Awake() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("PlayerCollider")) {
            activeRespawnPoint = this;
        }
    }
}