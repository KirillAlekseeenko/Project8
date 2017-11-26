using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

	[SerializeField]
	private GameObject halo;

	[SerializeField]
	private GameObject iconPrefab;

	private GameObject icon;

	private NavMeshAgent navMeshAgent;

	private bool isSelected = false;

	public bool IsSelected {
		get {
			return isSelected;
		}
		set {
			isSelected = value;
		}
	}

	public GameObject IconPrefab {
		get {
			return iconPrefab;
		}
	}

	public GameObject Icon {
		get {
			return icon;
		}
		set {
			icon = value;
		}
	}

	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	public void showHalo()
	{
		halo.SetActive (true);
	}
	public void hideHalo()
	{
		halo.SetActive (false);
	}

	public void moveTo(Vector3 goal) // destination in world's system 
	{
		navMeshAgent.destination = goal;
	}

}
