using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movement;
    public Rigidbody2D rigid;
    public float speed = 7.0f;
    public bool isFacingRight = true;
    bool jumpPressed = false;
    public float jumpForce = 500.0f;
    public LayerMask whatIsGround; //what should be considered ground for jumping off
    public float groundDistance = 1.5f;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
        //called once per frame, used for user input
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    //called multiple times per frame, best for physics for smooth behavior
    {
        rigid.velocity = new Vector2 (movement * speed, rigid.velocity.y);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
        {
            Flip();
        }
        if (jumpPressed && isOnGround())
            Jump();
    }

    void Flip()
    {
      Vector3 playerScale = transform.localScale;
      playerScale.x = playerScale.x * -1;
      transform.localScale = playerScale;
      isFacingRight = !isFacingRight; 

       // transform.Rotate(new Vector3(0, 180, 0));
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        rigid.AddForce(new Vector2(0, jumpForce));
        jumpPressed = false;
    }

    bool isOnGround()
    //see, e.g.: https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
    {
        // return rigid.velocity.y == 0;
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        Debug.DrawRay(position, direction, Color.green);

        RaycastHit2D hit = Physics2D.Raycast(position, direction, groundDistance, whatIsGround);
        if (hit.collider == null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        return grounded;
    }
}
