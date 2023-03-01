using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool Grounded;

    private float dirX = 0f;
    [SerializeReference] private float moveSpeed = 7f;
    [SerializeReference] private float jumpForce = 14f;

    private enum MovementState { idle,running,jumping,falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        Debug.DrawRay(transform.position, Vector3.down * 2.0f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 2.0f))
        {
            //Debug.Log("ground");
            Grounded = true;
        }
        else
        {
            //Debug.Log(" not ground");
            Grounded = false;
        }
        if (Input.GetButtonDown("Jump")&&Grounded)
        {

            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }
        UpdateAnimationUpdate();
        
    }
    
    private void UpdateAnimationUpdate()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
}
