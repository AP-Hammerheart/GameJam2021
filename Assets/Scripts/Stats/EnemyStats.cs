using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    void Start()
    {
        // DamagePopUpTextController.Initialize();
    }
    public override void TakeDamage(float TotalDamage)
    {
        // DamagePopUpTextController.CreateFloatingText(TotalDamage.ToString(), transform);
        base.TakeDamage(TotalDamage);
    }



    public override void Die()
    {
        base.Die();
        Rigidbody rigidbody;
        rigidbody = GetComponent<Rigidbody>();

        //GetComponent<EnemyController>().enabled = false;
        //GetComponent<EnemyAttack>().enabled = false;
        //GetComponent<Collider>().enabled = false;
        //Destroy(rigidbody);
        Destroy(gameObject);



    }
}
