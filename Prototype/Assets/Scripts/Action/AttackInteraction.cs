using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackInteraction : Interaction {

	private float longRangeAttackRadius;
	private int longRangeAttackDamage;

	private NavMeshAgent navMeshAgentComponent;

	private Vector3 targetPosition;

	public AttackInteraction(Unit actionOwner, Unit actionReceiver)
	{
		this.actionOwner = actionOwner;
		this.actionReceiver = actionReceiver;
		this.longRangeAttackRadius = actionOwner.LongRangeAttackRadius;
		this.longRangeAttackDamage = actionOwner.RangeAttack;
		this.navMeshAgentComponent = actionOwner.GetComponent<NavMeshAgent> ();
		this.targetPosition = actionReceiver.transform.position;
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
			if (actionReceiver == null) {
				navMeshAgentComponent.ResetPath ();
				return new ActionState (true, -1);
			}
				

			var vectorToTarget = actionReceiver.transform.position - actionOwner.transform.position;
			float unitWidth = 0.5f;

			var ray = new Ray (actionOwner.transform.position, vectorToTarget);
			var rayLength = Mathf.Min (longRangeAttackRadius, vectorToTarget.magnitude + unitWidth);
			RaycastHit hit;

			if (!Physics.Raycast (ray, out hit, rayLength, ~LayerMask.GetMask("Unit")) // проверка на отсутствие препятствий
				&& rayLength < longRangeAttackRadius) { 

				navMeshAgentComponent.ResetPath (); // остановка
				if((actionOwner as Unit).Fire())
					(actionReceiver as Unit).SufferDamage (longRangeAttackDamage);

			} else { 
				if (targetPosition != actionReceiver.transform.position || !navMeshAgentComponent.hasPath) { // положение цели изменилось или юнит не имеет никаких указаний
					targetPosition = actionReceiver.transform.position;
					navMeshAgentComponent.SetDestination (targetPosition);
				}
			}

			return new ActionState (false, -1);
		}
	}

	#endregion



}
