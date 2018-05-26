using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
	public static float Distance(Unit from, Unit to)
	{
		return Direction(from, to).magnitude;
	}
	public static Vector3 Direction(Unit from, Unit to)
	{
		return to.transform.position - from.transform.position;
	}

    public static float Distance(Vector3 position1, Vector3 position2, bool x = true, bool y = true, bool z = true)
    {
        var squaredDeltaX = x ? Mathf.Pow(position1.x - position2.x, 2.0f) : 0;
        var squaredDeltaY = y ? Mathf.Pow(position1.y - position2.y, 2.0f) : 0;
        var squaredDeltaZ = z ? Mathf.Pow(position1.z - position2.z, 2.0f) : 0;

        return Mathf.Sqrt(squaredDeltaX + squaredDeltaY + squaredDeltaZ);
    }
}
