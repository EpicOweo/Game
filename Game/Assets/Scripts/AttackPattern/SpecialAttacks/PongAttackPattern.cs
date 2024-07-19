using System.Collections;
using UnityEngine;

public class PongAttackPattern : AttackPattern {

    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject ball;

    Rigidbody2D ballRb;


    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject topWall;
    public GameObject bottomWall;


    public GameObject ballPrefab;
    Vector3 initialPos;

    public readonly float minBallVelX = 30;
    public static readonly float ballConstMagnitude = 50;

    Rigidbody2D leftPaddleRb;
    Rigidbody2D rightPaddleRb;

    float leftMovementRand;
    float rightMovementRand;
    float lastBallVelX;


    void Awake() {
        ballRb = ball.GetComponent<Rigidbody2D>();
        leftPaddleRb = leftPaddle.GetComponent<Rigidbody2D>();
        rightPaddleRb = rightPaddle.GetComponent<Rigidbody2D>();
    }
    
    public void CreateNewBall(bool firstTime) {
        if(!firstTime) ball = Instantiate(ballPrefab, initialPos, Quaternion.identity);

        ballRb = ball.GetComponent<Rigidbody2D>();

        int xRand = Random.Range(0, 2);
        int dir = xRand == 0 ? 1 : -1;
        
        int yRand = Random.Range(0, 2);
        int yDir = yRand == 0 ? 1 : -1;
        float yVel = Random.Range(3f, 6f);
        yVel *= yDir;

        ballRb.velocity = new Vector2(dir * minBallVelX, yVel);
        lastBallVelX = ballRb.velocity.x;

        leftMovementRand = Random.Range(0.75f, 1.25f);
        rightMovementRand = Random.Range(0.75f, 1.25f);

        ball.transform.SetParent(transform);
    }

    public override void Run() {
        transform.position = Player.instance.transform.position;
        transform.position += new Vector3(0, 2, 0); // so it spawns a little above the player

        initialPos = transform.position;

        leftPaddle.SetActive(true);
        rightPaddle.SetActive(true);
        ball.gameObject.SetActive(true);

        CreateNewBall(true);

        Level.instance.mainCamera.GetComponentInChildren<CameraController>().Lock(transform.position);
    }

    void UpdatePaddle(GameObject paddle) {
        float movementRand = paddle == leftPaddle ? leftMovementRand : rightMovementRand;
        Vector2 lerpedPos = Vector2.Lerp(paddle.transform.position, ball.transform.position, 4 * Time.deltaTime);
        paddle.transform.position = new(paddle.transform.position.x, lerpedPos.y);

        Rigidbody2D rb = paddle.GetComponent<Rigidbody2D>();

        int paddleVel = 13;
        float deadzoneSize = 1f;

        if(Mathf.Abs(ball.transform.position.y - paddle.transform.position.y) > deadzoneSize) {
            if(ball.transform.position.y > paddle.transform.position.y) {
                rb.velocity = paddleVel * Vector2.up;
            } else {
                rb.velocity = -paddleVel * Vector2.up;
            }
        } else {
            rb.velocity = new(0, 0);
        }
        

       /* paddle.GetComponent<Rigidbody2D>().velocity = new(
            0,
            6*(ball.transform.position.y - paddle.transform.position.y)
        );*/

    }

    void Update() {

        
        

        if(Mathf.Sign(ballRb.velocity.x) != Mathf.Sign(lastBallVelX)) {
            leftMovementRand = Random.Range(0.75f, 1.25f);
            rightMovementRand = Random.Range(0.75f, 1.25f);
            Debug.Log("true");
        }

        lastBallVelX = ballRb.velocity.x;

        if(ballRb.velocity.x > 0) {
            if(ballRb.velocity.x < minBallVelX) {
                ballRb.velocity = new(minBallVelX, ballRb.velocity.y);
            }
            leftPaddleRb.velocity = Vector2.zero;
            UpdatePaddle(rightPaddle);
            
        } else if(ballRb.velocity.x < 0) {
            if(ballRb.velocity.x > -minBallVelX) {
                ballRb.velocity = new(-minBallVelX, ballRb.velocity.y);
            }
            rightPaddleRb.velocity = Vector2.zero;
            UpdatePaddle(leftPaddle);
        }

        if(ballRb.velocity.y == 0) {
            ballRb.velocity += Vector2.up;
        }

    }


    public IEnumerator Stop() {
        Level.instance.mainCamera.GetComponentInChildren<CameraController>().Unlock();
        yield return null;
    }
}