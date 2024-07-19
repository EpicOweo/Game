using UnityEngine;

public class TestPlayer : MonoBehaviour {

    public Rigidbody2D rb;
    
    public float speed = 10;

    void Update() {

        float velX = 0;
        float velY = 0;


        if(Input.GetKey(KeyCode.W)) {
            velY = speed;
        } else if(Input.GetKey(KeyCode.S)) {
            velY = -speed;
        }

        if(Input.GetKey(KeyCode.A)) {
            velX = -speed;
        } else if(Input.GetKey(KeyCode.D)) {
            velX = speed;
        }

        rb.velocity = new(velX, velY);
    }
}
