using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour {

	[SerializeField] float time;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, time);
	}
	

}
