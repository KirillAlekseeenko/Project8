using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour { // hotkeys and shift-selection

	[SerializeField]
	private SelectionHandler selectionHandler;

	[SerializeField]
	private FieldOfViewHandler fieldOfViewHandler;

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			selectionHandler.IsShiftDown = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			selectionHandler.IsShiftDown = false;
		}

		if (Input.GetKeyDown (KeyCode.C)) {
			fieldOfViewHandler.IsAltOn = true;
		}
		if (Input.GetKeyUp (KeyCode.C)) {
			fieldOfViewHandler.IsAltOn = false;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			selectionHandler.Perks.ActivatePerk (0);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			selectionHandler.Perks.ActivatePerk (1);
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			// third perk
		}



	}

}
