using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (
			transform.position.x - Input.GetAxis("Horizontal"),
			transform.position.y - Input.GetAxis("Mouse ScrollWheel"),
			transform.position.z - Input.GetAxis("Vertical")
		);
	}
}
