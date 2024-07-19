using System.Collections;
using Settings;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : PlayerComponent
{

    public Keybinds keybinds => GameManager.instance.keybinds;


    public int moveSpeed;
    public int moveAcceleration;

    public float dashSpeed;
    public float dashTime;

    public float fallThroughTapThreshold;


    private bool dashing = false;
    private bool canDash = false;
    private float dashTimer = 0;

    private float fallThroughTimer = 0;
    
    
    public override void Init() {
        base.Init();
        keybinds.PlayerMovement.FallThrough.performed += StartOnFallThrough;
        keybinds.PlayerMovement.Jump.performed += OnJump;
    }

    void Update() {
        DoMovement();
    }

    public PlayerInputComponent Clone() {
        PlayerInputComponent inputComponent = Instantiate(prefab).GetComponent<PlayerInputComponent>();

        return inputComponent;
    }

    void DoMovement() {
        if(dashing) return;
        Rigidbody2D rb = Player.instance.rb;
        
        InputAction horizontalMove = keybinds.PlayerMovement.HorizontalMovement;
        int direction = Mathf.RoundToInt(horizontalMove.ReadValue<float>());


        int speed = moveSpeed;

        if(direction != 0) {

            Player.instance.onWalk.Invoke(direction);
            
            float sign = rb.velocity.x != 0
                ? Mathf.Abs(rb.velocity.x) / rb.velocity.x : 0;

            if(Mathf.Abs(rb.velocity.x) < speed || sign != direction) { // sign != value: trying to turn around
                float acc = moveAcceleration;
                if(sign != direction) {
                    acc *= 2;
                }
                rb.velocity += Vector2.right * direction * acc * Time.deltaTime;
                if(Mathf.Abs(rb.velocity.x) > speed) {
                    rb.velocity = new (sign * speed, rb.velocity.y);
                }
            }
        } else {

            if(Mathf.Abs(rb.velocity.x) > 0) {
                float sign = Mathf.Abs(rb.velocity.x) / rb.velocity.x;
                rb.velocity += Vector2.right * -sign * moveAcceleration * Time.deltaTime;
                float newSign = Mathf.Abs(rb.velocity.x) / rb.velocity.x;
                if(sign != newSign) {
                    rb.velocity = new (0, rb.velocity.y);
                }
                
            }
        }
    }

    void StartOnFallThrough(InputAction.CallbackContext ctx) {
        StartCoroutine(OnFallThrough());
    }

    IEnumerator OnFallThrough() {
        Physics2D.IgnoreCollision(Player.instance.boxCollider, Level.instance.platformTilemapCollider2D);

        bool unIgnore = false;
        keybinds.PlayerMovement.FallThrough.canceled += (_) => { unIgnore = true; };

        fallThroughTimer = Time.time;

        bool tap = true;

        yield return new WaitUntil(() => {

            if(Time.time - fallThroughTimer >= fallThroughTapThreshold) {
                tap = false;
            }

            if(unIgnore) {
                bool colliding = Player.instance.boxCollider.IsTouching(Level.instance.platformTilemapCollider2D);
                return !colliding;
            } else {
                return false;
            }
        });

        keybinds.PlayerMovement.FallThrough.canceled -= (_) => { unIgnore = true; };
        if(tap) {
            yield return new WaitForSeconds(0.15f);
        }
        
        Physics2D.IgnoreCollision(Player.instance.boxCollider, Level.instance.platformTilemapCollider2D, false);
    }

    void OnJump(InputAction.CallbackContext ctx) {
        /*
        if(canDash) {
            Dash();
            canDash = false;
            dashing = true;
            dashTimer = Time.time;
            rb.gravityScale = 0;
        }*/
        Player.instance.flipped = !Player.instance.flipped;
        Player.instance.onFlipped.Invoke();
    }

    void Dash() {
        InputAction horizontalMove = keybinds.PlayerMovement.HorizontalMovement;
        int horizontal = Mathf.RoundToInt(horizontalMove.ReadValue<float>());
        InputAction verticalMove = keybinds.PlayerMovement.VerticalMovement;
        int vertical = Mathf.RoundToInt(verticalMove.ReadValue<float>());
        
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        Player.instance.rb.velocity = dashSpeed * direction;

        canDash = false;
        dashing = true;
        dashTimer = Time.time;
        Player.instance.rb.gravityScale = 0;
    }

    public override void CleanUp() {
        base.CleanUp();

        // set defaults
        dashing = false;
        canDash = false;
        dashTimer = 0;
        fallThroughTimer = 0;
    }
}