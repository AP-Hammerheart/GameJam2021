using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSegment : MonoBehaviour
{
    public IntVector2 coordinates;

    private FloorCellEdge[] edges = new FloorCellEdge[FloorDirections.Count];

    private int initializedEdgeCount;

    public bool IsFullyInitialized;

    public MapSegmentDirection RandomUninitializedDirection { get; internal set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
