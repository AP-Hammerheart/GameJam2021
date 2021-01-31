using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private SphereCollider sphColl;
    private SpriteRenderer render;
    public string Item;
    public Sprite[] sprites;
    public bool test;

    private void Awake()
    {
        sphColl = GetComponent<SphereCollider>();
        render = GetComponent<SpriteRenderer>();
        Item = "";
        if (test)
            Initialize();
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
            Debug.Log("Collllliiiided with plaaayuer");

            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();

            if (Item == "GUN")
            {
                Debug.Log("GUN PICKED UPPPPP");
                playerStats.PickUpGUN();
            }
            if (Item == "SWORD")
            {
                Debug.Log("SWORD PICKED UPPPPP");
                playerStats.PickUpSWORD();
            }
        }

        Destroy(gameObject, 1);
    }
}