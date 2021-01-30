using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    Animator animator;
    bool isOpen = false;
    bool startOpen = false;
    bool startClose = false;
    float closeTimeMin = 5f;
    float closeTimeMax = 15f;
    float openTimeMin = 0f;
    float openTimeMax = 30f;
    float openTime = 30f;
    float closeTime = 15f;
    float timer = 0f;

    void Start() {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update() {
        if( startOpen ) {
            timer += Time.deltaTime;
            if( timer >= openTime ) {
                animator.Play( "Base Layer.Open" );
                startOpen = false;
                closeTime = Random.Range( closeTimeMin, closeTimeMax );
                startClose = true;
                timer = 0f;
            }
        } else if( startClose ) {
            timer += Time.deltaTime;
            if( timer >= openTime ) {
                animator.Play( "Base Layer.Close" );
                startClose = false;
                timer = 0f;
            }
        }
    }

    private void OnTriggerEnter( Collider other ) {
        if( other.tag == "Player" && !startOpen && !startClose ) {
            openTime = Random.Range( openTimeMin, openTimeMax );
            startOpen = true;
            timer = 0f;
        }
    }
}
