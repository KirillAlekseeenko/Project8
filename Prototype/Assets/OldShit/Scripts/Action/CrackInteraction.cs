using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrackInteraction : Interaction
{
	private NavMeshAgent navMeshAgentComponent;
	private Hacker hackerComponent;

	public CrackInteraction(Unit actionOwner, WorldObject actionReceiver)
	{
		this.actionOwner = actionOwner;
		this.actionReceiver = actionReceiver;
		hackerComponent = actionOwner.GetComponent<Hacker>();
		navMeshAgentComponent = actionOwner.GetComponent<NavMeshAgent>();
	}

	public override ActionState State
	{
		get
		{
			var actionOwnerPos = actionOwner.transform.position;
			var receiverPos = actionReceiver.transform.position;

			if (Utils.Distance(actionOwnerPos, receiverPos, y: false) < hackerComponent.CrackDistance)
			{
				(actionReceiver as ControlPanel).Activate(actionOwner.Owner);
                navMeshAgentComponent.ResetPath();
                return new ActionState(true, -1);
			}
			else
			{
				return new ActionState(false, -1);
			}
		}
	}

	public override void Finish()
	{
		navMeshAgentComponent.ResetPath();
	}

	public override void Perform()
	{
		navMeshAgentComponent.SetDestination(actionReceiver.transform.position);
	}
}
