using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody rb;
    public CharacterStats Stats;
    int damage = 12;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter(Collider hitInfo)
    {

        Debug.Log(hitInfo.name);
        if(hitInfo.tag == "Enemy")
        hitInfo.GetComponent<CharacterStats>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
