using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLocalAI : MonoBehaviour {

	private Unit unitComponent;

	private Stack<Unit> alarmStack;

	private bool canHeal;

	void Awake()
	{
		alarmStack = new Stack<Unit> ();
	}

	void Start()
	{
		canHeal = (GetComponent<Scientist> () != null);
		unitComponent = GetComponent<Unit> ();
		StartCoroutine (updateCoroutine());
	}


	private IEnumerator updateCoroutine()
	{
		while (true) {
			yield return new WaitForSeconds (0.2f);
			checkForEnemies ();
		}
	}
		
	[SerializeField]
	private int visibleObjectCount;

	private void checkForEnemies()
	{
		var colliders = Physics.OverlapSphere (transform.position, unitComponent.LOS, LayerMask.GetMask ("Unit"));

		Unit closestEnemyUnit = null;
		Unit closestFriendlyUnit = null;

		foreach (var collider in colliders) {
			Unit unit = collider.gameObject.GetComponent<Unit> ();
			if (unit != null) {
				var vectorToUnit = unit.transform.position - transform.position;
				RaycastHit hit;
				if (unitComponent.isFriend(unit)) {
					alarmStack.Push (unit);
					if(!unit.IsHealthy() && canHeal)
						if (closestFriendlyUnit == null || vectorToUnit.magnitude < (closestFriendlyUnit.transform.position - transform.position).magnitude)
							closestFriendlyUnit = unit;
				} else {
					if (!Physics.Raycast (new Ray (transform.position, vectorToUnit), out hit, vectorToUnit.magnitude,LayerMask.GetMask ("Building"))) {
						unit.SetVisible ();
						if(unitComponent.isEnemy(unit)) 
							if (closestEnemyUnit == null || vectorToUnit.magnitude < (closestEnemyUnit.transform.position - transform.position).magnitude)
								closestEnemyUnit = unit;
					} 
				}
			}
		}
		if (unitComponent.isIdle ()) {
			if (closestFriendlyUnit != null) {
				unitComponent.AssignAction (new HealInteraction (unitComponent, closestFriendlyUnit));
			} else if (closestEnemyUnit != null) {
				foreach (var unit in alarmStack) {
					if (unit.isIdle ()) {
						unit.AssignAction (new AttackInteraction(unit, closestEnemyUnit));
					}
				}
				unitComponent.AssignAction (new AttackInteraction (unitComponent, closestEnemyUnit));
			}
		}



		alarmStack.Clear ();
	}
		
}
