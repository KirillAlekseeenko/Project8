using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

	[SerializeField]
	private float alarmRadius;

	HashSet<Unit> units;
	HashSet<Unit> idleUnits;

	HashSet<Building> buildingsOnLevel;
	HashSet<Building> capturedBuildings;

	[SerializeField]
	List<Path> patrolPaths;

	void Awake()
	{
		units = new HashSet<Unit> ();
		idleUnits = new HashSet<Unit> ();
	}

	void Start()
	{
		
	}

	void Update()
	{
		if (idleUnits.Count > 0) {
			engageUnits ();
		}
	}

	private void engageUnits()
	{
		int averageCount = idleUnits.Count / patrolPaths.Count;
		int modulo = idleUnits.Count % patrolPaths.Count;

		int i = 0, j = 0;
		foreach (var unit in idleUnits) {
			patrolPaths [i].assignPath (unit);
			j++;
			if (j == averageCount) {
				j = 0;
				i = (i == patrolPaths.Count) ? i : i + 1;
			}
		}

		idleUnits.Clear ();

	}

	public void Alarm(Unit enemy, Transform origin)
	{
		foreach (Unit unit in units) {
			if (Vector3.Distance (unit.transform.position, origin.transform.position) < alarmRadius) {
				if(!unit.isAttacking())
					unit.AssignAction(new AttackInteraction(unit, enemy));
			}
		}
	}

	public void spawnedUnit(Unit unit)
	{
		units.Add (unit);
	}

	public void becomeIdle(Unit unit)
	{
		int i = Random.Range(0, patrolPaths.Count);
		patrolPaths [i].assignPath (unit);
	}

	public void deadUnit(Unit unit)
	{
		units.Remove (unit);
		idleUnits.Remove (unit);
	}
}
