using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private SphereCollider sphColl;
    private SpriteRenderer render;
    private string Item;
    public Sprite[] sprites;

    private void Awake()
    {
        sphColl = GetComponent<SphereCollider>();
        render = GetComponent<SpriteRenderer>();
        Item = "";
    }

    public void Initialize()
    {
        int x = Random.Range(0, 2);

        if (x == 0)
        {
            SetItemString("GUN");
            render.sprite = sprites[x];
        }
        if (x == 1)
        {
            SetItemString("SWORD");
            render.sprite = sprites[x];
        }
    }

    public void SetItemString(string item)
    {
        Item = item;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();

            if (Item == "GUN")
            {
                playerStats.PickUpGUN();
            }
            if (Item == "SWORD")
            {
                playerStats.PickUpSWORD();
            }
        }

        Destroy(gameObject, 1);
    }
}