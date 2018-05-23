using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour
{
    public float speed = 5;
    
    private Rigidbody2D myBody;
    private SpriteRenderer sprite;
    private Animator animator;
    private int layer_id;

    private bool isGrounded;

    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        layer_id = 1 << LayerMask.NameToLayer("Ground");
    }

    void FixedUpdate()
    {
        float valueX = Input.GetAxis("Horizontal");
        Vector2 vel = myBody.velocity;
        if (Mathf.Abs(valueX) > 0)
        {
            vel.x = valueX * speed;
            myBody.velocity = vel;
            sprite.flipX = valueX < 0;
            animator.SetBool ("running", true);
        }
        else
        {
            animator.SetBool ("running", false);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vel.y = speed;
            myBody.velocity = vel;
        }
       
        RaycastHit2D hit = Physics2D.Linecast(transform.position+Vector3.up * 0.3f, transform.position + Vector3.down * 0.1f, layer_id);
        isGrounded = hit;
        animator.SetBool("jump", !isGrounded);
    }
    

    void Update()
    {
    }
}