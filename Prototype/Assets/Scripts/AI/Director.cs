using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour {

	[SerializeField] private float alarmRadius;

	HashSet<Unit> units;
	HashSet<Unit> idleUnits;

	HashSet<Building> buildingsOnLevel;
	HashSet<Building> capturedBuildings;

    List<HashSet<Unit>> unitGroups;


	[SerializeField]
	List<Path> patrolPaths;

	void Awake()
	{
		idleUnits = new HashSet<Unit> ();
        buildingsOnLevel = new HashSet<Building>();
        capturedBuildings = new HashSet<Building>();
        unitGroups = new List<HashSet<Unit>>();
	}

	void Start()
	{
		
	}

	void Update()
	{
		if (idleUnits.Count > 0) {
            if(capturedBuildings.Count > 0) // есть захваченные здания
            {
                // собрать группу и направить ее на здание
            }
            else
            {
                engageUnits();
            }
		}
	}

	private void engageUnits()
	{
		int averageCount = idleUnits.Count / patrolPaths.Count;
		int modulo = idleUnits.Count % patrolPaths.Count;

		int i = 0, j = 0;
		foreach (var unit in idleUnits) {
			patrolPaths [i].AssignPath (unit, 20);
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
				if (!unit.isAttacking ()) {
					unit.AssignAction (new AttackInteraction (unit, enemy));
				}
			}
		}
	}

	public void spawnedUnit(Unit unit)
	{
		if (units == null) {
			units = new HashSet<Unit> (); 
		} else {
			units.Add (unit);
		}
	}

	public void becameIdle(Unit unit)
	{
		if (patrolPaths.Count == 0)
			return;
		int i = Random.Range(0, patrolPaths.Count);
		patrolPaths [i].AssignPath (unit, 20);
	}

	public void deadUnit(Unit unit)
	{
		units.Remove (unit);
		idleUnits.Remove (unit);
	}
}
