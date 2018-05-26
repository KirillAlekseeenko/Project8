using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCamera : MonoBehaviour {

    private const float speedStep = 0.05f;

    [SerializeField] private float sensivity;
    [SerializeField] private float speed;

    private float horizontalRotation;
    private float verticalRotation;

    private float forwardTranslation;
    private float rightTranslation;



	private void Start()
	{
        horizontalRotation = transform.rotation.eulerAngles.x;
        verticalRotation = transform.rotation.eulerAngles.y;
	}

	private void Update()
	{
        getInput();
	}

	private void LateUpdate()
	{
        transform.Translate(new Vector3(rightTranslation, 0, forwardTranslation) * speed);
        transform.localRotation = Quaternion.Euler(new Vector3(horizontalRotation, verticalRotation));
	}

    private void getInput()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
            speed += speedStep;
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            speed -= speedStep;
            if (speed < 0)
                speed = 0;
        }

        verticalRotation += Input.GetAxis("Mouse X") * sensivity;
        horizontalRotation += -Input.GetAxis("Mouse Y") * sensivity;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -80, 80);

        forwardTranslation = Input.GetAxis("Vertical");
        rightTranslation = Input.GetAxis("Horizontal");

    }

}
