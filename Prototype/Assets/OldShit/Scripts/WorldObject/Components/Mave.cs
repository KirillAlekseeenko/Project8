using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mave : MonoBehaviour {

	[SerializeField] private GameObject startPoint;


	public void MoveToStart(){
		transform.position = startPoint.transform.position;
	}
}
