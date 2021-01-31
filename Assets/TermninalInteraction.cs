using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TermninalInteraction : MonoBehaviour
{
    public ParticleSystem particles;
    private void OnTriggerEnter( Collider other ) {
        if( other.tag == "Player" ) {
            particles.Play();
        }
    }
}
