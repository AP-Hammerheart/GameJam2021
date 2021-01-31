using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public override void Awake()
    {

        base.Awake();
    }


    public override void TakeDamage(float TotalDamage)
    {
        anim.SetTrigger("hurt");
        base.TakeDamage(TotalDamage);
    }

    public override void Die()
    {
        anim.SetBool("dead", true);
        anim.SetTrigger("die");
        base.Die();


        // Respawn after time, regenerate the map
    }

}
