using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float Basedamage = 10;
    public float armor = 0;
    public Animator anim;
    bool dead = false;
    public HP_bar hpBar;
    public float maxHealth = 100;
    public float currentHealth { get; private set; }
    public float TotalDamage;

    public virtual void Awake()
    {

        currentHealth = maxHealth;
        hpBar.SetMaxHealth((int)maxHealth);
        TotalDamage = Basedamage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }

    public virtual void TakeDamage(float TotalDamage)
    {

        TotalDamage -= armor;

        TotalDamage = Mathf.Clamp(TotalDamage, 0, float.MaxValue);

        currentHealth -= TotalDamage;
        Debug.Log(transform.name + " takes " + TotalDamage + " damage.");

        hpBar.SetHealth((int)currentHealth);

        if (currentHealth <= 0)
        { 
            if(dead == false)
            {
                Die();
            }
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        dead = true;
        Debug.Log(transform.name + " Died.");
    }
}
