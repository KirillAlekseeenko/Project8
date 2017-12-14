using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocalAI : MonoBehaviour {

	private Unit unitComponent;


	void Start()
	{
		unitComponent = GetComponent<Unit> ();
	}



	void Update()
	{
		if (unitComponent.isIdle()) {
			checkForEnemies ();
		}
	}

	private void checkForEnemies()
	{

		var colliders = Physics.OverlapSphere (transform.position, unitComponent.pLOS, LayerMask.GetMask("Unit"));

		foreach (var collider in colliders) {
			var unit = collider.gameObject.GetComponent<Unit> ();
			if (unit == null)
				continue;
			if (!unit.Owner.IsHuman) {
				var vectorToEnemy = unit.transform.position - transform.position;
				if (!Physics.Raycast (new Ray (transform.position, vectorToEnemy), unitComponent.pLOS, ~LayerMask.GetMask("Unit"))
					&& vectorToEnemy.magnitude < unitComponent.pLOS) {

					unitComponent.AssignAction (new AttackInteraction (unitComponent, unit));


				}
			}
		}

	}
}
