using Assets.Scripts.NewLevelGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : Randomizer
{
    public enum Direction // HEADING
    {
        Right, Left
    }

    #region prefabs
    //large
    public MapSegment largeLeftEndPrefab;
    public MapSegment largeMiddleSinglePrefab;
    public MapSegment largeMiddleSingleWPrefab;
    public MapSegment largeRightEndPrefab;
    public MapSegment largeToSmallPrefab;
    //SMALL
    public MapSegment SmallLeftEndPrefab;
    public MapSegment SmallLeftStaircaseW1Prefab;
    public MapSegment SmallMiddleSinglePrefab;
    public MapSegment SmallMiddleSingleWPrefab;
    public MapSegment SmallRightEndPrefab;
    public MapSegment SmallRightStaircasePrefab;
    public MapSegment SmallToLargePrefab;

    #endregion



    public MapSegment lastCreated;
    public MapSegment[] mapSegmentPrefabs;
    //public IntVector2 size;
    public float generationStepDelay;
    private List<MapSegment> allSegments;
    public int segmentAmount;
    public int wildcard;
    private Direction dir;
    private Vector3 whereToSpawn;

    public IEnumerator GenerateMap(Transform startPos, int roundNumber)
    {
        dir = Flip();
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        allSegments = new List<MapSegment>();
        whereToSpawn = startPos.position;
        FirstStepGen(whereToSpawn, roundNumber);

        for (int x = 0; x < segmentAmount; x++)
        {
            whereToSpawn = new Vector3(whereToSpawn.x + 5f, whereToSpawn.y, 0f);

            if (x == segmentAmount - 1)
                LastStepGen(whereToSpawn, roundNumber);
            else
            {
                CreateMapSegment(whereToSpawn, mapSegmentPrefabs[(UnityEngine.Random.Range(0, mapSegmentPrefabs.Length))]);
            }
        }
        yield return delay;
    }

    private Direction Flip()
    {
        int x = UnityEngine.Random.Range(0, 1);
        if (x == 0)
            //Heading right
            return Direction.Right;
        else
            //Heading Left
            return Direction.Left;
    }

    private MapSegment CreateMapSegment(Vector3 pos, MapSegment mapSegment)
    {
        MapSegment ms = Instantiate(mapSegment, pos, Quaternion.identity);
        ms.name = "MapSegment: " + pos;
        ms.transform.parent = transform;
        allSegments.Add(ms);
        UpdateNextPos(ms);
        lastCreated = ms;
        return ms;
    }

    private void FirstStepGen(Vector3 pos, int roundNumber)
    {
        //Heading right
        if (dir == Direction.Right)
        {
            if (roundNumber == 1)
                CreateMapSegment(pos, largeLeftEndPrefab);

            if ()
            {

            }


        }
        CreateMapSegment(pos, startSegment);
    }

    private void UpdateNextPos(MapSegment newestSeg)
    {
        if (dir == Direction.Right)
        {
            var t = newestSeg.transform.localScale;
            Vector3 diff = new Vector3((t.x + 5), newestSeg.transform.position.y, 0);
            whereToSpawn = diff;
        }
        else
        {
            var t = newestSeg.transform.localScale;
            Vector3 diff = new Vector3((t.x - 5), newestSeg.transform.position.y, 0);
            whereToSpawn = diff;
        }
    }

    private void LastStepGen(Vector3 pos, int roundNumber)
    {
        if (roundNumber == 10)
            if (dir == Direction.Right)
                CreateMapSegment(pos, largeRightEndPrefab);

        if(dir == Direction.Right)
            CreateMapSegment(pos, largeRightEndPrefab);
    }

    private MapSegment DetermineFirstKloss()
    {
        if (dir == Direction.Right)
            return SmallToLargePrefab;
        else
            return largeToSmallPrefab;
    }

}
