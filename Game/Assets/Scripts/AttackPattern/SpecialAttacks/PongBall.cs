using System.Collections;
using UnityEngine;

public class PongBall : MonoBehaviour {

    public Rigidbody2D rb;

    
    public Vector3 initialPos;

    int damage;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Player.instance.health.Damage(damage);
        } else if(collision.gameObject.name == "Left" || collision.gameObject.name == "Right") {
            GetComponentInParent<PongAttackPattern>().CreateNewBall(firstTime: false);
            Destroy(gameObject);
        } else {
            StartCoroutine(SetVelOnCol(collision));
        }
    }

    IEnumerator SetVelOnCol(Collision2D collision) {
        yield return new WaitForEndOfFrame();
        if(Random.Range(0f, 1f) >= 0f) {
            rb.velocity = new Vector2(
            rb.velocity.x,
            10 * (transform.position.y-collision.transform.position.y)
            ).normalized * PongAttackPattern.ballConstMagnitude;
        } else {
            rb.velocity = rb.velocity.normalized * PongAttackPattern.ballConstMagnitude;
        }
        
    }
}