using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    public float movement;
    public float movement2;
    public Rigidbody2D rigid;
    public float speed = 7.0f;//Plane Speed
    public bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        //Um what this do?
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        movement2 = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    //called multiple times per frame, best for physics for smooth behavior
    {
        rigid.velocity = new Vector2(movement * speed, movement2 * speed);
        if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 playerScale = transform.localScale;
        playerScale.x = playerScale.x * -1;
        transform.localScale = playerScale;
        isFacingRight = !isFacingRight;

        // transform.Rotate(new Vector3(0, 180, 0));
    }
}
