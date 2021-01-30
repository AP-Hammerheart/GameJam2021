using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public MapSegment mapSegmentPrefab;
    private MapSegment[,] mapSegments;
    public IntVector2 size;
    public float generationStepDelay;

    public IEnumerator GenerateMap()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        mapSegments = new MapSegment[size.x, size.y];
        List<MapSegment> segments = new List<MapSegment>();
        DoFirstGenerationStep(segments);
        while (segments.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(segments);
        }
        yield return delay;
    }

    private void DoNextGenerationStep(List<MapSegment> activeSegments)
    {
        int currentIndex = activeSegments.Count - 1;
        MapSegment currentSegment = activeSegments[currentIndex];

        if (currentSegment.IsFullyInitialized)
        {
            activeSegments.RemoveAt(currentIndex);
            return;
        }
        var direction = currentSegment.RandomUninitializedDirection;

        IntVector2 coordinates = currentSegment.coordinates + direction.ToIntVector2();

        if (ContainsCoordinates(coordinates))
        {
            MapSegment neighbor = GetSegment(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateSegment(coordinates);
                //CreatePassage(currentCell, neighbor, direction);
                //activeCells.Add(neighbor);
            }
            else
            {
                //CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            //CreateWall(currentCell, null, direction);
        }

    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
    }

    private void DoFirstGenerationStep(List<MapSegment> activeSegments)
    {
        activeSegments.Add(CreateSegment(RandomCoordinates));
    }

    private MapSegment CreateSegment(IntVector2 coordinates)
    {
        MapSegment newMapSegment = Instantiate(mapSegmentPrefab) as MapSegment;
        mapSegments[coordinates.x, coordinates.y] = newMapSegment;
        newMapSegment.coordinates = coordinates;
        newMapSegment.name = "Map Segment: " + coordinates.x + ", " + coordinates.y;
        newMapSegment.transform.parent = transform;
        newMapSegment.transform.localPosition = new Vector3(coordinates.x - size.x, coordinates.y - size.y, 0f);
        return newMapSegment;
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.y));
        }
    }
    public MapSegment GetSegment(IntVector2 coordinates)
    {
        return mapSegments[coordinates.x, coordinates.y];
    }

}
