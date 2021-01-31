using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject BulletPrefab;
    private PlayerStats stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && stats.gunPickedUp)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject t = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Destroy(t, 1);
    }
}
