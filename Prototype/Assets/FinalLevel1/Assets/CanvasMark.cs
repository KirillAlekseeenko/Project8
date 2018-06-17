using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMark : MonoBehaviour {

	private Canvas canvas;

	protected void Start(){
		canvas = gameObject.GetComponent<Canvas> ();
	}

	void Update(){
		updateCanvasPosition ();
	}

	private void updateCanvasPosition()
	{
		var cameraLook = -Camera.main.transform.forward;
		canvas.transform.rotation = Quaternion.LookRotation(-cameraLook);
	}
}
