using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol2 : MonoBehaviour
{
    public float range = 3f;
    public float speed = 1f;
    public bool facingRight = true;
    Vector3 p;
    public float coolDownValue = 5f;
    public float coolDown = 0;
    private Vector3 velocity = Vector3.zero;
    public Rigidbody rb;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    // Start is called before the first frame update
    void Awake()
    {

        p = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        Vector3 current_position = transform.position;

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector3(speed, 0, 0);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

        if (current_position.x > p.x + range && coolDown < 0)
        {
            //rb.velocity = Vector3.zero;
            Flip();
            speed = -speed;
            coolDown = coolDownValue;
        }

        if (current_position.x < p.x - range && coolDown < 0)
        {
            //rb.velocity = Vector3.zero;
            Flip();
            speed = -speed;
            coolDown = coolDownValue;

        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        if (facingRight == true)
            transform.eulerAngles = new Vector3(0, -180, 0);
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        facingRight = !facingRight;
    }
}
