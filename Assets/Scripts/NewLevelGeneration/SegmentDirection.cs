using UnityEngine;

public enum MapSegmentDirection
{
	North,
	East,
	South,
	West
}

public static class MapSegmentDirections
{

	public const int Count = 4;

	public static MapSegmentDirection RandomValue
	{
		get
		{
			return (MapSegmentDirection)Random.Range(0, Count);
		}
	}

	private static MapSegmentDirection[] opposites = {
		MapSegmentDirection.South,
		MapSegmentDirection.West,
		MapSegmentDirection.North,
		MapSegmentDirection.East
	};

	public static MapSegmentDirection GetOpposite(this MapSegmentDirection direction)
	{
		return opposites[(int)direction];
	}

	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};

	public static IntVector2 ToIntVector2(this MapSegmentDirection direction)
	{
		return vectors[(int)direction];
	}

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 0f, 90f),
		Quaternion.Euler(0f, 0f, 180f),
		Quaternion.Euler(0f, 0f, 270f)
	};

	public static Quaternion ToRotation(this MapSegmentDirection direction)
	{
		return rotations[(int)direction];
	}
}