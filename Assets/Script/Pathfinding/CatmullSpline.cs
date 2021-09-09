using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CatmullSpline
{
	public List<Vector3> navigationPoints = new List<Vector3>();
		
	[SerializeField] private int _connectionID;
	[SerializeField] private Color _gizmosColor = Color.white;
	public Color SetGizmosColor(Color newColor) => _gizmosColor = newColor;

	public CatmullSpline(int connectionID)
    {
		_connectionID = connectionID;
	}

	// Get a point on a curve between P0-P3 where t is betweeon 0 and 1 and represents how far along the line we've traveled. Some math genius figured this one out. Might as well summon a demon.
	public static Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		Vector3 a = 2f * p1;
		Vector3 b = p2 - p0;
		Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
		Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

		Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

		return pos;
	}

	//Return length of single spline 
	public static float DisplayCatmullRomSpline(int pos, Path path)
	{
		Vector3 p0 = path.controlPoints[WrapListPos(pos - 1, path.controlPoints)];
		Vector3 p1 = path.controlPoints[WrapListPos(pos, path.controlPoints)];
		Vector3 p2 = path.controlPoints[WrapListPos(pos + 1, path.controlPoints)];
		Vector3 p3 = path.controlPoints[WrapListPos(pos + 2, path.controlPoints)];

		Vector3 lastPos = p1;

		float resolution = 0.1f; // value between 0 and 1. Must be able to add up to 1.
		float length = 0;

		// Times to loop at specified resolution
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			float t = i * resolution;

			Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

			Gizmos.DrawLine(lastPos, newPos);

			length += Vector3.Distance(lastPos, newPos);
			lastPos = newPos;
		}
		return length; //return length of complete spline.
	}

	private static int WrapListPos<T>(int index, List<T> list)
	{
		if (index < 0)
		{
			index = list.Count;
		}
		if (index > list.Count)
		{
			index = 1;
		}
        else if (index > list.Count - 1)
        {
            index = 0;
        }
        return index;
	}
}