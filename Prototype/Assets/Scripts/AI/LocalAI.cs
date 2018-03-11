using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalAI : MonoBehaviour {

	private Unit unitComponent;
	private Director director;

	private float timeForIdleness = 1.0f;
	private float time = 0;

	void Start()
	{
		unitComponent = GetComponent<Unit> ();
		if (!unitComponent.Owner.IsHuman) {
			director = unitComponent.Owner.GetComponent<Director> ();
			director.spawnedUnit (unitComponent);
		}
	}

	void Update()
	{
		if (!unitComponent.isAttacking ()) {
			checkForEnemies ();
			checkIdleness ();
		}
	}

	void OnDestroy()
	{
		director.deadUnit (unitComponent);
	}

	private void checkForEnemies()
	{
		
		var colliders = Physics.OverlapSphere (transform.position, unitComponent.pLOS, LayerMask.GetMask("Unit"));

		foreach (var collider in colliders) {
			var unit = collider.gameObject.GetComponent<Unit> ();
			if (unit == null)
				continue;
			if (unitComponent.isEnemy(unit)) {
				var vectorToEnemy = unit.transform.position - transform.position;
				if (!Physics.Raycast (new Ray (transform.position, vectorToEnemy), vectorToEnemy.magnitude, LayerMask.GetMask("Building")) // проверка на отсутствие препятствий
					&& (vectorToEnemy.magnitude < unitComponent.pLOS * RTS.Constants.HearRadiusCoefficient || isObjectInsideTheArc(unitComponent, unit) || unit.isAttacking())) { // + еще условия связанные с конусом, кругом слышимости и инвизом у юнита

					unitComponent.AssignAction (new AttackInteraction (unitComponent, unit));

					director.Alarm (unit, unitComponent.transform); // зовет всех на помощь (радиус у всех одинаковый и является свойством экземпляра Director)
					
				}
			}
		}
	}

	private void checkIdleness()
	{
		if (unitComponent.isIdle ()) {
			time += Time.deltaTime;
			if (time >= timeForIdleness) {
				director.becameIdle (unitComponent);
				time = 0;
			}
		} else {
			if (time > 0)
				time = 0;
		}
	}

	private bool isObjectInsideTheArc(Unit unit, Unit enemy)
	{
		var vectorToEnemy = enemy.transform.position - unit.transform.position;
		if (unit.HalfVisible && vectorToEnemy.magnitude > unit.pLOS / 2)
			return false;
		
		var direction = unit.transform.TransformDirection (Vector3.forward);
		return Vector3.Angle (vectorToEnemy, direction) < RTS.Constants.VisionArcAngle / 2;

	}



}
