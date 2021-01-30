using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public MapGenerator generator;
    private MapGenerator generatorInstance;

    void Start()
    {
        BeginGame();
    }

    private void BeginGame()
    {
        generatorInstance = Instantiate(generator) as MapGenerator;
        StartCoroutine(generatorInstance.GenerateMap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
