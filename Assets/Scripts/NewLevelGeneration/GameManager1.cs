using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    public MapGenerator generator;
    private MapGenerator generatorInstance;
    public int roundNumber;

    void Start()
    {
        roundNumber = 1;
        BeginGame();
    }

    private void BeginGame()
    {
        generatorInstance = Instantiate(generator) as MapGenerator;
        StartCoroutine(generatorInstance.GenerateMap(transform, roundNumber));
    }

    private void NewLevel()
    {
        StopAllCoroutines();
        roundNumber++;
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
