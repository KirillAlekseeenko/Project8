using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealInteraction : Interaction {

	private float healRadius;

	private NavMeshAgent navMeshAgentComponent;
	private Scientist scientistComponent;

	private Vector3 targetPosition;

	public HealInteraction(Unit actionOwner, Unit actionReceiver)
	{
		this.actionOwner = actionOwner;
		this.actionReceiver = actionReceiver;

		this.navMeshAgentComponent = actionOwner.GetComponent<NavMeshAgent> ();
		this.targetPosition = actionReceiver.transform.position;
		this.scientistComponent = actionOwner.GetComponent<Scientist> ();
		this.healRadius = scientistComponent.HealRadius;
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
			if (actionReceiver == null || (actionReceiver as Unit).IsHealthy()) {
				navMeshAgentComponent.ResetPath ();
				return new ActionState (true, -1);
			}

			var vectorToTarget = actionReceiver.transform.position - actionOwner.transform.position;
			float unitWidth = 0.5f;

			var ray = new Ray (actionOwner.transform.position, vectorToTarget);
			var rayLength = Mathf.Min (healRadius, vectorToTarget.magnitude + unitWidth);
			RaycastHit hit;

			if (!Physics.Raycast (ray, out hit, rayLength, ~LayerMask.GetMask("Unit")) && rayLength < healRadius) { // проверка на отсутствие препятствий

				navMeshAgentComponent.ResetPath (); // остановка
				actionOwner.transform.LookAt(actionReceiver.transform.position);  // поворот

				scientistComponent.PerformHeal (actionReceiver as Unit);
				// heal

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
