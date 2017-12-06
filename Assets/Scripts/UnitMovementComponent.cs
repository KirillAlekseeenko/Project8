using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementComponent : MonoBehaviour {

	private SelectionHandler selectionHandler;

	void Start()
	{
		selectionHandler = GetComponent<SelectionHandler> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (1)){

			var selectedUnits = selectionHandler.SelectedObjects;

			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit) && selectedUnits != null){
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
					// there should be a particle system or sprite to indicate the destination point
					foreach (Unit unit in selectedUnits) {
						unit.moveTo (hit.point); // for multiple selection there must be some formation to avoid crowding in this point
					}
				}
			}

		}
		
	}
}
