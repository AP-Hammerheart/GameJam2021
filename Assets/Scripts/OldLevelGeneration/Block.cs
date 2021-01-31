using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void SetColor(Color color) {
        transform.GetChild( 0 ).GetComponent<Renderer>().material.color = color;
    }
}
