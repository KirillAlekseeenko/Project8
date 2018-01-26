using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[SerializeField] Camera camera; 

	private float movementSpeed;
	private float sideThickness;

	private delegate void MoveFunctionDelegate();
	private MoveFunctionDelegate moveFunction;

	// Use this for initialization
	void Awake () {
		movementSpeed = RTS.Constants.CameraMovementSpeed;
		sideThickness = RTS.Constants.CameraMovementSideThickness;
	}

	public float MovementSpeed {
		get {
			return movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}
	
	// Update is called once per frame
	void Update () {
		moveCamera ();
	}

	private void moveCamera()
	{
		var mousePosition = Input.mousePosition;

		moveFunction = null;

		if (mousePosition.x < Screen.width * sideThickness || Input.GetKey(KeyCode.A))
			moveFunction += moveLeft;
		if (mousePosition.y < Screen.height * sideThickness || Input.GetKey(KeyCode.S))
			moveFunction += moveBack;
		if (mousePosition.x > Screen.width * (1 - sideThickness) || Input.GetKey(KeyCode.D))
			moveFunction += moveRight;
		if (mousePosition.y > Screen.height * (1 - sideThickness) || Input.GetKey(KeyCode.W))
			moveFunction += moveForward;

		if(moveFunction != null)
			moveFunction ();
	}

	private void moveRight ()
	{
		camera.transform.Translate (Vector3.right * movementSpeed * Time.deltaTime, Space.World);
	}
	private void moveLeft ()
	{
		camera.transform.Translate (Vector3.left * movementSpeed * Time.deltaTime, Space.World);
	}
	private void moveBack ()
	{
		camera.transform.Translate (Vector3.back * movementSpeed * Time.deltaTime, Space.World);
	}
	private void moveForward ()
	{
		camera.transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime, Space.World);
	}
}
