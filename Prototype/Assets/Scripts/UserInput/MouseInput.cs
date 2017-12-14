using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseInput : MonoBehaviour {

	[SerializeField]
	private EventSystem eventSystem;

	[SerializeField]
	private SelectionHandler selectionHandler;

	[SerializeField]
	private SelectionBoxDrawComponent selectionBoxDrawer;

	[SerializeField]
	private ActionHandler actionHandler;



	private Vector3 clickPosition;
	[SerializeField]
	private float dragThreshold;
	private bool isLeftMouseButtonDown = false;
	private bool isDrag = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		selectionHandler.OnMouseHover (Input.mousePosition);

		if (Input.GetMouseButtonDown (0)) {
			if(!eventSystem.IsPointerOverGameObject ())
			{
				isDrag = false;
				clickPosition = Input.mousePosition;
				isLeftMouseButtonDown = true;
				selectionHandler.OnLeftButtonDown (clickPosition);
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			isLeftMouseButtonDown = false;
			isDrag = false;
			selectionHandler.OnLeftButtonUp (Input.mousePosition);
			selectionBoxDrawer.StopDrawing ();
		}

		if (isLeftMouseButtonDown && Vector3.Distance (Input.mousePosition, clickPosition) > dragThreshold) {
			isDrag = true;

			selectionHandler.OnDrag (clickPosition, Input.mousePosition);
			selectionBoxDrawer.DrawRectangle (clickPosition, Input.mousePosition);
		}

		if (Input.GetMouseButtonDown (1)) {
			
			actionHandler.AssignAction (Input.mousePosition);
		}
			
		
	}

}
