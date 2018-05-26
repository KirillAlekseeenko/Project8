using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

	// Use this for initialization

	public void SpawnUnit(Unit unit)
	{
		Instantiate (unit.gameObject, transform.position, Quaternion.identity);
	}
}
