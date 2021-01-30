using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FloorDirections;

public class Floor : MonoBehaviour
{
    public IntVector2 size;
    public FloorCell cellPrefab;
    public FloorPassage passagePrefab;
    public FloorWall wallPrefab;
    private FloorCell[,] cells;
    public float generationStepDelay;


    public FloorCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.y];
    }

    public IEnumerator GenerateMap()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new FloorCell[size.x, size.y];

        List<FloorCell> activeCells = new List<FloorCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
    }

    private void DoNextGenerationStep(List<FloorCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        FloorCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        FloorDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            FloorCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private FloorCell CreateCell(IntVector2 coordinates)
    {
        FloorCell newCell = Instantiate(cellPrefab) as FloorCell;
        cells[coordinates.x, coordinates.y] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Floor Cell " + coordinates.x + ", " + coordinates.y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, coordinates.y - size.y * 0.5f + 0.5f, 0f);
        return newCell;
    }

    private void CreatePassage(FloorCell cell, FloorCell otherCell, FloorDirection direction)
    {
        FloorPassage passage = Instantiate(passagePrefab) as FloorPassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as FloorPassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(FloorCell cell, FloorCell otherCell, FloorDirection direction)
    {
        FloorWall wall = Instantiate(wallPrefab) as FloorWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as FloorWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private void DoFirstGenerationStep(List<FloorCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.y));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
    }

}
