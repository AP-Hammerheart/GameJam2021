using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyAI : MonoBehaviour
{


    public float walkSpeed, range;
    private float distToPlayer;

    [HideInInspector]
    public bool mustPatrol;
    public bool mustTurn;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player;


    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }

        /*distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer <= range)
        {
            Shoot();
        }*/

        Debug.Log("test");
    }
    //private void FixedUpdate()
    //{
    //    if (mustPatrol)
    //    {
    //        mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
    //    }
    //}

    void Patrol()
    {
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }


    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

}
