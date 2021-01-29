using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Floor floorPrefab;
    private Floor floorInstance;

    void Start()
    {
        BeginGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    private void BeginGame()
    {
        floorInstance = Instantiate(floorPrefab) as Floor;
        StartCoroutine(floorInstance.GenerateMap());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(floorInstance);
    }
}