using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    Material material;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponentInChildren<Material>();
    }

    void SetColor(Color color) {
        material.color = color;
    }
}
