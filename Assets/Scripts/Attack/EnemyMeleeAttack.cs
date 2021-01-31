using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class EnemyMeleeAttack : MonoBehaviour
{
    public CharacterStats Stats;
    public Transform AttackPos;
    public LayerMask WhatIsEnemies;
    //public float AttackSpeed = 1f;
    private float Cooldown = 1f;
    private float CooldownCurrent = 0f;
    public float AttackRadius = 3f;
    // public Animator anim;
    Transform target;
    public GameObject MeleeAnim;
    public Transform MeleeAnimPos;

    void Awake()
    {
        Stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
            MeleeAttackAction();
            CooldownCurrent -= Time.deltaTime;
    }

    public void MeleeAttackAction()
    {

        //anim.SetTrigger("Attack");
        // Debug.Log("Melee ATTAAAACK");
        if (CooldownCurrent <= 0)
        {
            var t = Instantiate(MeleeAnim, MeleeAnimPos);
            Vector3 newSize = new Vector3(t.transform.localScale.x * 3, t.transform.localScale.x * 3, t.transform.localScale.x * 3);
            t.transform.localScale = newSize;
            t.transform.parent = transform;
            Collider [] enemiesToDamage = Physics.OverlapSphere(AttackPos.position, AttackRadius, WhatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {

                enemiesToDamage[0].GetComponent<CharacterStats>().TakeDamage(Stats.TotalDamage);
            }
            CooldownCurrent = Cooldown;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRadius);
    }
}
