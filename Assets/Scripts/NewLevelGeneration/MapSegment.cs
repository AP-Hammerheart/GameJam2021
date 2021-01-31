using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    public enum Kloss
    {
        largeLeftEnd, largeMiddleSingle, largeMiddleSingleW, largeRightEnd,
        largeToSmall, SmallLeftEnd, SmallLeftStaircaseW1, SmallMiddleSingle,
        SmallMiddleSingleW, SmallRightEnd, SmallRightStaircase, SmallToLarge
    }

    public GameObject[] objectsICanSpawn;
    public Transform[] spawnPoints;

    public Kloss thisKloss;
    private void Start()
    {
        thisKloss = new Kloss();
    }

    //public GameObject SpawnRandom()
    //{
    //    gameObject g = Instantiate
    //}

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
    }

}
