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
}
