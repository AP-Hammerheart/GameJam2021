using Assets.Scripts.NewLevelGeneration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapSegment;

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
    public MapSegment NextKloss;
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
        if (roundNumber != 1)
            dir = Flip();

        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        allSegments = new List<MapSegment>();
        whereToSpawn = startPos.position;
        FirstStepGen(whereToSpawn, roundNumber);

        for (int x = 0; x < segmentAmount; x++)
        {

            if (x == segmentAmount - 1)
                LastStepGen(roundNumber);
            else
            {
                CreateMapSegment();
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

    private MapSegment CreateMapSegment()
    {
        whereToSpawn = DetermineNextSpawnPosition();
        NextKloss =  DetermineNextKloss();
        Debug.Log(NextKloss);
        MapSegment ms = Instantiate(NextKloss, whereToSpawn, Quaternion.identity);
        ms.SetKloss(nameof(NextKloss));
        ms.name = $"MapSegment {nameof(NextKloss)} : " + NextKloss.transform.position;
        ms.transform.parent = transform;
        allSegments.Add(ms);
        //UpdateNextPos(ms);
        lastCreated = ms;
        return ms;
    }

    private void FirstStepGen(Vector3 pos, int roundNumber)
    {
        if (roundNumber == 1)
            CreateFirstMapSegment(pos, largeLeftEndPrefab);
        else
            CreateMapSegment();
    }

    private MapSegment CreateFirstMapSegment(Vector3 pos, MapSegment largeLeftEndPrefab)
    {
        MapSegment ms = Instantiate(DetermineFirstKloss(), pos, Quaternion.identity);
        ms.SetKloss(nameof(largeLeftEndPrefab));
        ms.name = "MapSegment: " + pos;
        ms.transform.parent = transform;
        allSegments.Add(ms);
        //UpdateNextPos(ms);
        lastCreated = ms;
        NextKloss = DetermineNextKloss();
        return ms;
    }

    //private void UpdateNextPos(MapSegment newestSeg)
    //{
    //    if (dir == Direction.Right)
    //    {
    //        var t = newestSeg.transform.localScale;
    //        Vector3 diff = new Vector3((t.x + 5), newestSeg.transform.position.y, 0);
    //        whereToSpawn = diff;
    //    }
    //    else
    //    {
    //        var t = newestSeg.transform.localScale;
    //        Vector3 diff = new Vector3((t.x - 5), newestSeg.transform.position.y, 0);
    //        whereToSpawn = diff;
    //    }
    //}

    private void LastStepGen(int roundNumber)
    {
        if (roundNumber == 10)
        {
            //tdodo
        }
        CreateMapSegment();
        //if (dir == Direction.Right)
        //    CreateMapSegment(DetermineNextSpawnPosition(), largeLeftEndPrefab);

        //if (dir == Direction.Right)
        //    CreateMapSegment(DetermineNextSpawnPosition(), largeRightEndPrefab);
    }

    private MapSegment DetermineFirstKloss()
    {
        if (dir == Direction.Right)
            return SmallToLargePrefab;
        else
            return largeToSmallPrefab;
    }

    private Vector3 DetermineNextSpawnPosition()
    {
        float xOffset = 0;
        float yOffset = 0;
        var _kloss = lastCreated.thisKloss;
        if (lastCreated.thisKloss == Kloss.largeLeftEnd)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.largeMiddleSingle || NextKloss.thisKloss == Kloss.largeMiddleSingleW)
                    xOffset = 10f;
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                    xOffset = 15f;
            }
        }
        if (lastCreated.thisKloss == Kloss.largeRightEnd)
        {
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.largeMiddleSingle || NextKloss.thisKloss == Kloss.largeMiddleSingleW)
                    xOffset = -10f;
                if (NextKloss.thisKloss == Kloss.SmallToLarge)
                    xOffset = -15f;
            }
        }
        if (lastCreated.thisKloss == Kloss.largeMiddleSingle || lastCreated.thisKloss == Kloss.largeMiddleSingleW)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.largeMiddleSingle || NextKloss.thisKloss == Kloss.largeMiddleSingleW)
                    xOffset = 5f;
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                    xOffset = 10f;
                if (NextKloss.thisKloss == Kloss.largeRightEnd)
                    xOffset = 15f;
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.largeMiddleSingle || NextKloss.thisKloss == Kloss.largeMiddleSingleW)
                    xOffset = -5f;
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                    xOffset = -10f;
                if (NextKloss.thisKloss == Kloss.largeLeftEnd)
                    xOffset = -15f;
            }
        }
        if (lastCreated.thisKloss == Kloss.largeToSmall)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                    xOffset = 10f;
                if (NextKloss.thisKloss == Kloss.SmallRightEnd)
                    xOffset = 20f;
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase || NextKloss.thisKloss == Kloss.SmallToLarge || NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                    xOffset = 15f;
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.largeMiddleSingle || NextKloss.thisKloss == Kloss.largeMiddleSingleW)
                    xOffset = -10f;
                if (NextKloss.thisKloss == Kloss.largeLeftEnd || NextKloss.thisKloss == Kloss.SmallToLarge)
                    xOffset = -15f;
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallLeftEnd)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                    xOffset = 15f;
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1 || NextKloss.thisKloss == Kloss.SmallRightStaircase || NextKloss.thisKloss == Kloss.SmallToLarge)
                    xOffset = 20f;
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallLeftStaircaseW1)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1 || NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                {
                    xOffset = 10f;
                    yOffset = -5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallRightEnd)
                {
                    xOffset = 20f;
                    yOffset = -5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase || NextKloss.thisKloss == Kloss.SmallToLarge)
                {
                    xOffset = 15f;
                    yOffset = -5f;
                }
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.SmallLeftEnd)
                    xOffset = -20f;
                if (NextKloss.thisKloss == Kloss.largeLeftEnd || NextKloss.thisKloss == Kloss.SmallToLarge || NextKloss.thisKloss == Kloss.SmallRightStaircase)
                {
                    xOffset = -15f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                    xOffset = -10f;
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallMiddleSingle || lastCreated.thisKloss == Kloss.SmallMiddleSingleW)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                {
                    xOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1 || NextKloss.thisKloss == Kloss.SmallRightStaircase || NextKloss.thisKloss == Kloss.SmallToLarge)
                {
                    xOffset = 10f;
                }
                if (NextKloss.thisKloss == Kloss.SmallRightEnd)
                {
                    xOffset = 15f;
                }
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                    xOffset = 5f;
                if (NextKloss.thisKloss == Kloss.largeToSmall || NextKloss.thisKloss == Kloss.SmallToLarge || NextKloss.thisKloss == Kloss.SmallRightStaircase)
                {
                    xOffset = -10f;
                }
                if (NextKloss.thisKloss == Kloss.SmallLeftEnd)
                    xOffset = -15f;
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                {
                    xOffset = -10f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase)
                {
                    xOffset = -10f;
                    yOffset = -5f;
                }
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallRightEnd)
        {
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                    xOffset = -15f;
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                    xOffset = -20f;
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase || NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                {
                    xOffset = -20f;
                    yOffset = -5f;
                }
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallRightStaircase)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.SmallToLarge || NextKloss.thisKloss == Kloss.SmallRightStaircase|| NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                {
                    xOffset = 15f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallRightEnd)
                {
                    xOffset = 20f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                {
                    xOffset = 10f;
                    yOffset = 5f;
                }
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase)
                {
                    xOffset = -15f;
                    yOffset = -5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                {
                    xOffset = -15f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle || NextKloss.thisKloss == Kloss.SmallMiddleSingleW)
                {
                    xOffset = -10f;
                }
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingle)
                {
                    xOffset = -20f;
                }
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                {
                    xOffset = -15f;
                }
            }
        }
        if (lastCreated.thisKloss == Kloss.SmallToLarge)
        {
            if (dir == Direction.Right)
            {
                if (NextKloss.thisKloss == Kloss.largeToSmall)
                {
                    xOffset = 15f;
                }
                if (NextKloss.thisKloss == Kloss.largeMiddleSingleW || NextKloss.thisKloss == Kloss.largeMiddleSingle)
                {
                    xOffset = 10f;
                }
                if (NextKloss.thisKloss == Kloss.largeRightEnd)
                {
                    xOffset = 20f;
                }
            }
            if (dir == Direction.Left)
            {
                if (NextKloss.thisKloss == Kloss.SmallRightStaircase)
                {
                    xOffset = -15f;
                    yOffset = -5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallLeftStaircaseW1)
                {
                    xOffset = -15f;
                    yOffset = 5f;
                }
                if (NextKloss.thisKloss == Kloss.SmallMiddleSingleW || NextKloss.thisKloss == Kloss.SmallMiddleSingle)
                {
                    xOffset = -10f;
                }
                if (NextKloss.thisKloss == Kloss.SmallLeftEnd)
                {
                    xOffset = -20f;
                }

            }
        }
        //float newX = (lastCreated.transform.position.x + lastCreated.transform.localScale.x);
        //    float newY = (lastCreated.transform.position.y + lastCreated.transform.localScale.y);
        //    Vector3 newPos = new Vector3(newX, newY, 0);
        //    Debug.Log(newPos);
        //    return newPos;
        Debug.Log("lastCreatedKloss: " + lastCreated.thisKloss.ToString());
        Debug.Log("xOffset: " + xOffset.ToString());
        Debug.Log("yOffset: " + yOffset.ToString());
        float newX = lastCreated.transform.position.x + xOffset;
        float newY = lastCreated.transform.position.y + yOffset;
        Vector3 nextPos = new Vector3(newX, newY, 0);
        Debug.Log(nextPos);
        return nextPos;
    }
    private MapSegment DetermineNextKloss()
    {
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallToLarge)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 7);

                if (x == 1)//LargeToSmall
                    return largeToSmallPrefab;
                if (x == 2 || x == 5)//LargeMiddleSingleW
                    return largeMiddleSingleWPrefab;
                if (x == 3 || x == 6)//LargeMiddleSingle
                    return largeMiddleSinglePrefab;
                if (x == 4)//LargeRightEnd
                    return largeRightEndPrefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 11);

                if (x == 1)//smallMiddleSingle
                    return SmallMiddleSinglePrefab;
                if (x == 2 || x == 5)//LargeMiddleSingleW
                    return SmallMiddleSinglePrefab;
                if (x == 3 || x == 6)//LargeMiddleSingle
                    return SmallMiddleSingleWPrefab;
                if (x == 4)//LargeRightEnd
                    return SmallLeftEndPrefab;
                if (x == 7)//SmallLeftStaircaseW1
                    return SmallRightStaircasePrefab;
                if (x == 10)//SmallLeftStaircaseW1
                    return SmallLeftStaircaseW1Prefab;
                if (x == 8 || x == 9)//LargeToSmall
                    return largeToSmallPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallRightStaircase)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 8);

                if (x == 1)//LargeToSmall
                    return largeToSmallPrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallRightEndPrefab;
                if (x == 4)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 5)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 6 || x == 7)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;

            }
            else
            {
                int x = UnityEngine.Random.Range(1, 7);

                if (x == 1)//LargeToSmall
                    return largeToSmallPrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 4)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 5)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 6)//LargeToSmall
                    return SmallLeftEndPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallRightEnd)
        {
            if (dir == Direction.Right)
                return null;
            else
            {
                int x = UnityEngine.Random.Range(1, 6);

                if (x == 1)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 2)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 3)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 4)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 5)//LargeToSmall
                    return largeToSmallPrefab;

            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallMiddleSingleW)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 8);

                if (x == 1)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 2)//LargeToSmall
                    return SmallToLargePrefab;
                if (x == 3)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 4)//LargeToSmall
                    return SmallRightEndPrefab;
                if (x == 5)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 6 || x == 7)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 6);

                if (x == 1)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallLeftEndPrefab;
                if (x == 4)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 5)//LargeToSmall
                    return largeToSmallPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallMiddleSingle)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 8);

                if (x == 1)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 2)//LargeToSmall
                    return SmallToLargePrefab;
                if (x == 3)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 4)//LargeToSmall
                    return SmallRightEndPrefab;
                if (x == 5)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 6 || x == 7)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 6);

                if (x == 1)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallLeftEndPrefab;
                if (x == 4)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 5)//LargeToSmall
                    return largeToSmallPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.SmallLeftStaircaseW1)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 8);

                if (x == 1)//LargeToSmall
                    return SmallToLargePrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallRightEndPrefab;
                if (x == 4)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 5)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 6 || x == 7)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 6);

                if (x == 1)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 2)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 3)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 4)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 5)//LargeToSmall
                    return largeToSmallPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.largeToSmall)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 9);

                if (x == 1)//LargeToSmall
                    return SmallToLargePrefab;
                if (x == 2)//LargeToSmall
                    return SmallRightStaircasePrefab;
                if (x == 3)//LargeToSmall
                    return SmallRightEndPrefab;
                if (x == 4)//LargeToSmall
                    return SmallMiddleSinglePrefab;
                if (x == 5)//LargeToSmall
                    return SmallMiddleSingleWPrefab;
                if (x == 6 || x == 7)//LargeToSmall
                    return SmallLeftStaircaseW1Prefab;
                if (x == 8)//LargeToSmall
                    return SmallLeftEndPrefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 5);

                if (x == 1)//LargeToSmall
                    return SmallToLargePrefab;
                if (x == 2)//LargeToSmall
                    return largeMiddleSingleWPrefab;
                if (x == 3)//LargeToSmall
                    return largeMiddleSinglePrefab;
                if (x == 4)//LargeToSmall
                    return largeLeftEndPrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.largeMiddleSingleW)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 4);

                if (x == 1)//LargeToSmall
                    return largeMiddleSinglePrefab;
                if (x == 2)//LargeToSmall
                    return largeRightEndPrefab;
                else//LargeToSmall
                    return largeToSmallPrefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 4);

                if (x == 1)//LargeToSmall
                    return largeMiddleSinglePrefab;
                if (x == 2)//LargeToSmall
                    return largeLeftEndPrefab;
                else//LargeToSmall
                    return SmallToLargePrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.largeRightEnd)
        {
            if (dir == Direction.Right)
                return null;
            else
            {
                int x = UnityEngine.Random.Range(1, 4);

                if (x == 1)//LargeToSmall
                    return largeMiddleSinglePrefab;
                if (x == 2)//LargeToSmall
                    return largeMiddleSingleWPrefab;
                else//LargeToSmall
                    return SmallToLargePrefab;
            }
        }
        if (lastCreated.thisKloss == MapSegment.Kloss.largeMiddleSingle)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 4);

                if (x == 1)//LargeToSmall
                    return largeMiddleSingleWPrefab;
                if (x == 2)//LargeToSmall
                    return largeRightEndPrefab;
                else//LargeToSmall
                    return largeToSmallPrefab;
            }
            else
            {
                int x = UnityEngine.Random.Range(1, 4);

                if (x == 1)//LargeToSmall
                    return largeMiddleSingleWPrefab;
                if (x == 2)//LargeToSmall
                    return largeLeftEndPrefab;
                else//LargeToSmall
                    return SmallToLargePrefab;
            }

        }
        if (lastCreated.thisKloss == MapSegment.Kloss.largeLeftEnd)
        {
            if (dir == Direction.Right)
            {
                int x = UnityEngine.Random.Range(1, 5);

                if (x == 1)//LargeToSmall
                    return largeMiddleSingleWPrefab;
                if (x == 2)//LargeToSmall
                    return largeMiddleSinglePrefab;
                if (x == 3)//LargeToSmall
                    return largeToSmallPrefab;
                if (x == 4)//LargeToSmall
                    return largeMiddleSinglePrefab;
                else
                    return null;
            }
            else
                return null;
        }
        else
            return null;
    }
}