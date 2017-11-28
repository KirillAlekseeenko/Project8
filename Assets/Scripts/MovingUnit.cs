using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class MovingUnit : Unit {

	private bool isOnBoard = false;
	private Vector3 lastPositionBeforeTransport;
	[SerializeField]
	private Vector3 transport;

	public bool IsOnBoard {
		get {
			return isOnBoard;
		}
	}

	public void getOnBoard(Vector3 destination)
	{
		isOnBoard = true;
		lastPositionBeforeTransport = transform.position;
	}
	public void leftBoard()
	{
		isOnBoard = false;
		transform.position = lastPositionBeforeTransport;
	}

	private NavMeshAgent navMeshAgent; // moving common

	protected override void Awake()
	{
		base.Awake ();
		navMeshAgent = GetComponent<NavMeshAgent> (); // not common
	}


	public void moveTo(Vector3 goal) // destination in world's system      not common
	{
		navMeshAgent.destination = goal;
	}

	private IEnumerator moveCoroutine()
	{
		while (navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial)
			yield return new WaitForSeconds (1.0f);
		
	}

}
