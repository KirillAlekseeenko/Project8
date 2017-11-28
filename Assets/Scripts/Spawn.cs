using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	// Use this for initialization

	public void spawnUnit(MovingUnit unit)
	{
		Instantiate (unit, transform.position, Quaternion.identity);
	}
}
