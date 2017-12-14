using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour { // hotkeys and shift-selection

	[SerializeField]
	private SelectionHandler selectionHandler;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			selectionHandler.IsShiftDown = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			selectionHandler.IsShiftDown = false;
		}
	}

}
