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

	public override ActionState Finished {
		get {
			if (Vector3.Distance (actionOwner.transform.position, actionReceiver.transform.position) < enterRadius) {
				navMeshAgentComponent.ResetPath ();
				// здесь что-то вроде actionReceiver.getOnBoard(actionOwner)
				return new ActionState (true, -1);
			} else {
				return new ActionState (false, -1);
			}
		}
	}

	#endregion


}
