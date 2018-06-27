using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[SerializeField] Camera camera;

    [SerializeField] Vector3 cameraConstraints;

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
    public Vector3 ForwardVector { get { return Vector3.Cross(camera.transform.right, Vector3.up).normalized; } }
	
	// Update is called once per frame
	void Update () {
        if(InputModesHandler.CurrentMode != InputMode.UpgradeMode)
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
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            moveFunction += zoomIn;
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
            moveFunction += zoomOut;
        if(Input.GetMouseButton(2) || Input.GetKey(KeyCode.Q))
        {
            moveFunction += rotate;
        }

		if(moveFunction != null)
			moveFunction ();

        clampCameraPosition();

	}

	private void moveRight ()
	{
        camera.transform.Translate (Vector3.right * movementSpeed * Time.deltaTime, Space.Self);
	}
	private void moveLeft ()
	{
        camera.transform.Translate (Vector3.left * movementSpeed * Time.deltaTime, Space.Self);
	}
	private void moveBack ()
	{
        camera.transform.Translate (-ForwardVector * movementSpeed * Time.deltaTime, Space.World);
	}
	private void moveForward ()
	{
        camera.transform.Translate (ForwardVector * movementSpeed * Time.deltaTime, Space.World);
	}
    private void zoomIn()
    {
        camera.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.Self);
    }
    private void zoomOut()
    {
        camera.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.Self);
    }
    private void rotate()
    {
        camera.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * MovementSpeed, Space.World);
    }


    private void clampCameraPosition()
    {
        var currentPos = camera.transform.position;
        var x = Mathf.Clamp(currentPos.x, -cameraConstraints.x, cameraConstraints.x);
        var y = Mathf.Clamp(currentPos.y, -cameraConstraints.y, cameraConstraints.y);
        var z = Mathf.Clamp(currentPos.z, -cameraConstraints.z, cameraConstraints.z);
        camera.transform.position = new Vector3(x, y, z);
    }
}
