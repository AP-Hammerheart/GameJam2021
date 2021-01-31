using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager1 : MonoBehaviour
{
    public MapGenerator generator;
    private MapGenerator generatorInstance;
    public int roundNumber;
    public bool terminalSpawned;
    public GameObject Player;

    void Start()
    {
        roundNumber = 1;
        BeginGame();
    }

    private void BeginGame()
    {
        generatorInstance = Instantiate(generator) as MapGenerator;
        generatorInstance.wildcard = (UnityEngine.Random.Range(1, 10) + roundNumber);
        StartCoroutine(generatorInstance.GenerateMap(transform, roundNumber));
        Instantiate(Player, new Vector3(0, 2, 0), Quaternion.identity);
    }

    public void NewLevel()
    {
        StopAllCoroutines();
        roundNumber++;
        BeginGame();
    }

    public void Restart()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
