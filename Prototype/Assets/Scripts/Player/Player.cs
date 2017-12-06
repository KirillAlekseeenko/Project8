using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

	[SerializeField]
	private string username;  // задел по мультиплеер ))0)
	[SerializeField]
	private bool isHuman;

	void Start()
	{
		if (isHuman) {
			// user input and camera
		}
	}
}
