using UnityEngine;

public class FloatBehaviour : MonoBehaviour {
    

    public bool running = true;


    private Vector2 centralPivot;
    private Rigidbody2D rb;

    private float defaultAccel = 1.5f;
    private float accelY;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        centralPivot = rb.transform.position;

        float floatSpeed = 5;

        accelY = defaultAccel * floatSpeed;
        rb.velocity = new(0, -2);
    }

    void Update() {
        if(running) Accelerate();
    }

    void Accelerate() {
        float dist = rb.transform.position.y - centralPivot.y;
        float sign = -Mathf.Sign(dist);

        rb.velocity += Vector2.up * sign * accelY * Time.deltaTime;


        /*float maxVel = 2;

        if(Mathf.Abs(rb.velocity.y) > maxVel) {
            rb.velocity = Vector2.up * sign * maxVel;
        }*/
    }
}