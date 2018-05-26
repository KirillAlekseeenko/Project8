using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackInteraction : Interaction {

	private float rangeAttackRadius;
	private int rangeAttackDamage;

	private float meleeAttackRadius;
	private int meleeAttackDamage;

	private NavMeshAgent navMeshAgentComponent;

	private Vector3 targetPosition;

	public AttackInteraction(Unit actionOwner, Unit actionReceiver)
	{
		this.actionOwner = actionOwner;
		this.actionReceiver = actionReceiver;

		this.rangeAttackRadius = actionOwner.RangeAttackRadius;
		this.rangeAttackDamage = actionOwner.RangeAttack;
		this.meleeAttackDamage = actionOwner.MeleeAttack;
		this.meleeAttackRadius = actionOwner.MeleeAttackRadius;

		this.navMeshAgentComponent = actionOwner.GetComponent<NavMeshAgent> ();
		this.targetPosition = actionReceiver.transform.position;
	}
	
	#region implemented abstract members of Action

	public override void Perform ()
	{
		if (actionReceiver == null) {
			return;
		}
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

			var attackRadius = (actionOwner as Unit).IsRange ? rangeAttackRadius : meleeAttackRadius;
			var ray = new Ray (actionOwner.transform.position, vectorToTarget);
			var rayLength = Mathf.Min (attackRadius, vectorToTarget.magnitude + unitWidth);
			RaycastHit hit;

			if (!Physics.Raycast (ray, out hit, rayLength, ~LayerMask.GetMask("Unit")) && rayLength < attackRadius) { // проверка на отсутствие препятствий

				navMeshAgentComponent.ResetPath (); // остановка
				actionOwner.transform.LookAt(actionReceiver.transform.position);  // поворот
				if (rayLength < meleeAttackRadius || !(actionOwner as Unit).IsRange) {
					(actionOwner as Unit).PerformMeleeAttack (actionReceiver as Unit);
				} else {
					(actionOwner as Unit).PerformRangeAttack (actionReceiver as Unit);
				}

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
