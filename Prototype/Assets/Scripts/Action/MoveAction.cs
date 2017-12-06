using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAction : Action {

	static float threshold = 0.25f;

	private Vector3 destination;

	private NavMeshAgent navMeshAgentComponent;

	public MoveAction(Unit unit, Vector3 destination)
	{
		this.actionOwner = unit;
		this.destination = destination;
		this.navMeshAgentComponent = unit.GetComponent<NavMeshAgent> ();
	}

	#region implemented abstract members of Action

	public override void Perform ()
	{
		navMeshAgentComponent.SetDestination (destination);
	}

	public override void Finish ()
	{
		navMeshAgentComponent.ResetPath ();
	}

	public override ActionState Finished {
		get {
			return new ActionState (navMeshAgentComponent.remainingDistance < threshold, -1);
		}
	}

	#endregion

}