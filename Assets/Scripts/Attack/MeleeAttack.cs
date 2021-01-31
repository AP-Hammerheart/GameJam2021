using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class MeleeAttack : MonoBehaviour
{
        public CharacterStats Stats;
        public Transform AttackPos;
        public LayerMask WhatIsEnemies;
        //public float AttackSpeed = 1f;
        private float Cooldown = 0.5f;
        private float CooldownCurrent = 0f;
        public float AttackRadius = 3f;
        public Animator anim;
        Transform target;

        void Awake()
        {
            Stats = GetComponent<CharacterStats>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                MeleeAttackAction();
            }
            CooldownCurrent -= Time.deltaTime;
        }

        public void MeleeAttackAction()
        {

        // Debug.Log("Melee ATTAAAACK");
            if (CooldownCurrent <= 0)
            {
                anim.SetTrigger("MeleeAttack");
                Collider[] enemiesToDamage = Physics.OverlapSphere(AttackPos.position, AttackRadius, WhatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {

                    enemiesToDamage[i].GetComponent<CharacterStats>().TakeDamage(Stats.TotalDamage);
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
