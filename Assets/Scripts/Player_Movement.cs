using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Vector2 hitboxOffset;
    private PlayerLife CurrentPlayerLife;
    private bool IsAlive;

    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        CurrentPlayerLife = GetComponent<PlayerLife>();
        hitboxOffset = coll.offset;
    }

    // Update is called once per frame
    private void Update()
    {
        if(CurrentPlayerLife.IsAlive == false ) {
            return;
        }

        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * 8f, rb.velocity.y);
        coll = GetComponent<BoxCollider2D>();

        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, 10.5f);    
        }
        if (dirX > 0f) {
            sprite.flipX = true;
            coll.offset = new Vector2(-1 * hitboxOffset.x, hitboxOffset.y);
        } else if (dirX < 0f) {
            sprite.flipX = false;
            coll.offset = new Vector2(hitboxOffset.x, hitboxOffset.y);
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
