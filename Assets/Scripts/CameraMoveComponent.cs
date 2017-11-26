using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveComponent : MonoBehaviour {

	[SerializeField]
	private float cameraMoveSpeed;

	[SerializeField]
	private float cameraZoomSpeed;

	private float sideThickness = 0.01f;


	[SerializeField]
	private GameStateHandler gameStateHandler;

	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (!gameStateHandler.IsGamePaused)
				gameStateHandler.pauseGame ();
			else
				gameStateHandler.unpauseGame ();
		}

		//buttons

		if(Input.GetKey(KeyCode.A)){
			moveLeft ();
		}
		if(Input.GetKey(KeyCode.D)){
			moveRight ();
		}
		if(Input.GetKey(KeyCode.W)){
			moveForward ();
		}
		if(Input.GetKey(KeyCode.S)){
			moveBack ();
		}
		if(Input.GetKey(KeyCode.Q)){
			zoomIn ();
		}
		if(Input.GetKey(KeyCode.E)){
			zoomOut ();
		}


		// arrow is near one of the sides of the display

		var mousePosition = Input.mousePosition;

		if (mousePosition.x < Screen.width * sideThickness)
			moveLeft ();
		if (mousePosition.y < Screen.height * sideThickness)
			moveBack ();
		if (mousePosition.x > Screen.width * (1 - sideThickness))
			moveRight ();
		if (mousePosition.y > Screen.height * (1 - sideThickness))
			moveForward ();

		
	}


	public void moveRight ()
	{
		transform.Translate (Vector3.right * cameraMoveSpeed * Time.deltaTime, Space.World);
	}
	public void moveLeft ()
	{
		transform.Translate (Vector3.left * cameraMoveSpeed * Time.deltaTime, Space.World);
	}
	public void moveBack ()
	{
		transform.Translate (Vector3.back * cameraMoveSpeed * Time.deltaTime, Space.World);
	}
	public void moveForward ()
	{
		transform.Translate (Vector3.forward * cameraMoveSpeed * Time.deltaTime, Space.World);
	}
	public void zoomIn ()
	{
		transform.Translate (Vector3.forward * cameraMoveSpeed * Time.deltaTime);
	}
	public void zoomOut ()
	{
		transform.Translate (Vector3.back * cameraMoveSpeed * Time.deltaTime);
	}



	
}
