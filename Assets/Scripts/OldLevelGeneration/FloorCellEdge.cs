using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FloorDirections;

public abstract class FloorCellEdge : MonoBehaviour
{
	public FloorCell cell, otherCell;

	public FloorDirection direction;

	public void Initialize(FloorCell cell, FloorCell otherCell, FloorDirection direction)
	{
		this.cell = cell;
		this.otherCell = otherCell;
		this.direction = direction;
		cell.SetEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation();
	}
}