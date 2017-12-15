using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnterInteraction : Interaction {

	private float enterRadius; // вместо такого решения можно использовать свой collider для каждого здания
	private NavMeshAgent navMeshAgentComponent;

	public EnterInteraction(Unit unit, Building building, float enterRadius)
	{
		this.actionOwner = unit;
		this.actionReceiver = building;
		this.enterRadius = enterRadius;

		this.navMeshAgentComponent = unit.GetComponent<NavMeshAgent> ();
	}
	
	#region implemented abstract members of Action

	public override void Perform ()
	{
		navMeshAgentComponent.SetDestination (actionReceiver.transform.position);
	}

	public override void Finish ()
	{
		navMeshAgentComponent.ResetPath ();
	}

	public override ActionState State {
		get {
			var building = actionReceiver as Building;
			var unit = actionOwner as Unit;
			if (building.isUnitWithinTheEntrance(unit)) {
				navMeshAgentComponent.ResetPath ();
				building.AddUnit (unit);
				return new ActionState (true, -1);
			} else {
				return new ActionState (false, -1);
			}
		}
	}

	#endregion


}
