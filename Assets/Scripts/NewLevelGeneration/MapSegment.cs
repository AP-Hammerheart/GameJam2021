using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    public enum Kloss
    {
        largeLeftEnd, largeMiddleSingle, largeMiddleSingleW, largeRightEnd,
        largeToSmall, SmallLeftEnd, SmallLeftStaircaseW1, SmallMiddleSingle,
        SmallMiddleSingleW, SmallRightEnd, SmallRightStaircase, SmallToLarge, Elevator,
        ElevatorPlusSmallToLarge, ElevatorPlusLargeToSmall
    }

    public GameObject[] objectsICanSpawn;
    public GameObject[] enemiesICanSpawn;
    public Transform[] spawnPoints;

    public Kloss thisKloss;
    private void Start()
    {
        thisKloss = new Kloss();
        if (thisKloss != Kloss.SmallLeftStaircaseW1 && thisKloss != Kloss.SmallRightStaircase)
        {
            StartCoroutine(SpawnRandom());
        }
    }

    public IEnumerator SpawnRandom()
    {
        yield return new WaitForSeconds(0.5f);
        var gm = FindObjectOfType<GameManager1>();
        if (!gm.terminalSpawned && (gm.roundNumber == 5 || gm.roundNumber == 10 
            || gm.roundNumber == 15 || gm.roundNumber == 20 || gm.roundNumber == 25 || gm.roundNumber == 30))
        {
            GameObject g = Instantiate(objectsICanSpawn[2], spawnPoints[0].position, Quaternion.identity);
            gm.terminalSpawned = true;
        }
        else
        {
            int x = Random.Range(0, 20);
            if (x == 19 || x == 18)
            {
                //items
                int itemToSpawn = Random.Range(0, objectsICanSpawn.Length - 1);
                GameObject g = Instantiate(objectsICanSpawn[itemToSpawn], spawnPoints[0].position, Quaternion.identity);
                var t = g.GetComponent<PickableItem>();
                t.Initialize();
            }
            else
            {
                //enemies
                int enemyToSpawn = Random.Range(0, enemiesICanSpawn.Length - 1);
                GameObject g = Instantiate(enemiesICanSpawn[enemyToSpawn], spawnPoints[0].position, Quaternion.identity);
            }

        }
    }

    public void SetKloss(string klossname)
    {
        if (klossname == "largeLeftEndPrefab")
            thisKloss = Kloss.largeLeftEnd;
        else if (klossname == "largeMiddleSinglePrefab")
            thisKloss = Kloss.largeMiddleSingle;
        else if (klossname == "largeMiddleSingleWPrefab")
            thisKloss = Kloss.largeMiddleSingleW;
        else if (klossname == "largeRightEndPrefab")
            thisKloss = Kloss.largeRightEnd;
        else if (klossname == "largeToSmallPrefab")
            thisKloss = Kloss.largeToSmall;
        else if (klossname == "SmallLeftEndPrefab")
            thisKloss = Kloss.SmallLeftEnd;
        else if (klossname == "SmallLeftStaircaseW1Prefab")
            thisKloss = Kloss.SmallLeftStaircaseW1;
        else if (klossname == "SmallMiddleSinglePrefab")
            thisKloss = Kloss.SmallMiddleSingle;
        else if (klossname == "SmallMiddleSingleWPrefab")
            thisKloss = Kloss.SmallMiddleSingleW;
        else if (klossname == "SmallRightEndPrefab")
            thisKloss = Kloss.SmallRightEnd;
        else if (klossname == "SmallRightStaircasePrefab")
            thisKloss = Kloss.SmallRightStaircase;
        else if (klossname == "SmallToLargePrefab")
            thisKloss = Kloss.SmallToLarge;
        else if (klossname == "Elevator")
            thisKloss = Kloss.Elevator;
        else if (klossname == "ElevatorPlusSmallToLarge")
            thisKloss = Kloss.ElevatorPlusSmallToLarge;
        else if (klossname == "ElevatorPlusLargeToSmall")
            thisKloss = Kloss.ElevatorPlusLargeToSmall;
    }
}