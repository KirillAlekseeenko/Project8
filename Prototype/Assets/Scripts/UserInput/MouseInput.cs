using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour {

	[SerializeField] private EventSystem eventSystem;
	[SerializeField] private SelectionHandler selectionHandler;
	[SerializeField] private SelectionBoxDrawComponent selectionBoxDrawer;
	[SerializeField] private ActionHandler actionHandler;

	[SerializeField] private float dragThreshold;

	private Vector3 clickPosition;
	private bool isLeftMouseButtonDown = false;
	private bool isDrag = false;

	private bool isPerkModeOn = false;

	public void PerkModeOn()
	{
		isPerkModeOn = true;
	}
	public void PerkModeOff()
	{
		isPerkModeOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		selectionHandler.OnMouseHover (Input.mousePosition);

		if (Input.GetMouseButtonDown (0)) {
			if (isPerkModeOn) {
				selectionHandler.Perks.OnLeftButtonDown (Input.mousePosition);
			} else {
				if (!eventSystem.IsPointerOverGameObject ()) {
					isDrag = false;

					clickPosition = Input.mousePosition;
					isLeftMouseButtonDown = true;
					selectionHandler.OnLeftButtonDown (clickPosition);
				}
			}
		}

		if (Input.GetMouseButtonUp (0) && !isPerkModeOn) {
			isLeftMouseButtonDown = false;
			isDrag = false;

			selectionHandler.OnLeftButtonUp (Input.mousePosition);
			selectionBoxDrawer.StopDrawing ();
		}

		if (isLeftMouseButtonDown && Vector3.Distance (Input.mousePosition, clickPosition) > dragThreshold && !isPerkModeOn) {
			isDrag = true;

			selectionHandler.OnDrag (clickPosition, Input.mousePosition);
			selectionBoxDrawer.DrawRectangle (clickPosition, Input.mousePosition);
		}

		if (Input.GetMouseButtonDown (1)) {
			if (isPerkModeOn) {
				selectionHandler.Perks.OnRightButtonDown (Input.mousePosition);
			} else {
				actionHandler.AssignAction (Input.mousePosition);
			}
		}
			
		
	}

}
