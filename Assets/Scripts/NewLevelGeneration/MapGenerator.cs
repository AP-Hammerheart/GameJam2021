using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapSegment startSegment;
    public MapSegment endSegment;
    public MapSegment[] mapSegmentPrefabs;
    //public IntVector2 size;
    public float generationStepDelay;
    private List<MapSegment> allSegments;
    public int segmentAmount;

    public IEnumerator GenerateMap(Transform startPos)
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        allSegments = new List<MapSegment>();
        Vector3 whereToSpawn = startPos.position;
        FirstStepGen(whereToSpawn);

        for (int x = 0; x < segmentAmount; x++)
        {
            whereToSpawn = new Vector3(whereToSpawn.x + 5f, whereToSpawn.y, 0f);

            if (x == segmentAmount - 1)
                LastStepGen(whereToSpawn);
            else
            {
                CreateMapSegment(whereToSpawn, mapSegmentPrefabs[(UnityEngine.Random.Range(0, mapSegmentPrefabs.Length))]);
            }
        }
        yield return delay;
    }

    private MapSegment CreateMapSegment(Vector3 pos, MapSegment mapSegment)
    {
        MapSegment ms = Instantiate(mapSegment, pos, Quaternion.identity);
        ms.name = "MapSegment: " + pos;
        ms.transform.parent = transform;
        allSegments.Add(ms);
        return ms;
    }

    private void FirstStepGen(Vector3 pos)
    {
        CreateMapSegment(pos, startSegment);
    }

    private void LastStepGen(Vector3 pos)
    {
        CreateMapSegment(pos, endSegment);
    }
}
