using System.Collections;
using UnityEngine;

public class PlayerPhysicsComponent : PlayerComponent {

    
    public float terminalVelocity;
    public float zeroThreshold = 0.001f;


    public override void Init() {
        base.Init();
        Player.instance.onFlipped.AddListener(SwitchGravity);
    }

    void Update() {
        Player.instance.touchingGround = TouchingGround();
        Player.instance.touchingCeiling = TouchingCeiling();

        SetVelZeroIfUnderThreshold();
    }

    void LateUpdate() {
        DoTerminalVelocity();
    }

    public PlayerPhysicsComponent Clone() {
        PlayerPhysicsComponent physicsComponent = Instantiate(prefab).GetComponent<PlayerPhysicsComponent>();

        return physicsComponent;
    }

    void DoTerminalVelocity() {
        Rigidbody2D rb = Player.instance.rb;
        if(Mathf.Abs(rb.velocity.y) >= terminalVelocity) {
            float sign = rb.velocity.y != 0
                ? Mathf.Abs(rb.velocity.y) / rb.velocity.y : 0;

            rb.velocity = new(rb.velocity.x, sign * terminalVelocity);
        }
    }

    void SwitchGravity() {
        Player.instance.rb.gravityScale = -Player.instance.rb.gravityScale;
    }

    void SetVelZeroIfUnderThreshold() {
        Rigidbody2D rb = Player.instance.rb;
        if(Player.instance.touchingGround && Mathf.Abs(rb.velocity.y) <= zeroThreshold) {
            rb.velocity = new(rb.velocity.x, 0);
        }
    }

    bool TouchingGround() {
        Vector2 raycastPos = Player.instance.rb.position - Vector2.up * (Player.instance.boxCollider.size.y/2);

        Collider2D hit = Physics2D.OverlapBox(raycastPos, new Vector2(Player.instance.boxCollider.size.x - 0.1f, 0.2f),
            -90, LayerMask.GetMask("Terrain") | LayerMask.GetMask("Platforms"));
        
        return hit;
    }

    bool TouchingCeiling() {
        Vector2 raycastPos = Player.instance.rb.position + Vector2.up * (Player.instance.boxCollider.size.y/2);

        Collider2D hit = Physics2D.OverlapBox(raycastPos, new Vector2(Player.instance.boxCollider.size.x - 0.1f, 0.2f),
            90, LayerMask.GetMask("Terrain"));
        
        return hit;
    }
}