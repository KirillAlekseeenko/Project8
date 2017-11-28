using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitMovementComponent : MonoBehaviour {

	private SelectionHandler selectionHandler;

	[SerializeField]
	private EventSystem eventSystem;

	void Start()
	{
		selectionHandler = GetComponent<SelectionHandler> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (1) && !eventSystem.IsPointerOverGameObject()){

			var selectedUnits = selectionHandler.SelectedObjects;

			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit) && selectedUnits != null){
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
					// there should be a particle system or sprite to indicate the destination point
					foreach (MovingUnit unit in selectedUnits) {
						unit.moveTo (hit.point); // for multiple selection there must be some formation to avoid crowding in this point
					}
				}
				if (hit.collider.gameObject.layer == LayerMask.NameToLayer ("Selectable") && hit.collider.gameObject.GetComponent<Unit>().IsCapacious) {
					foreach (MovingUnit unit in selectedUnits) {
						unit.moveTo (hit.point); // for multiple selection there must be some formation to avoid crowding in this point
					}
				}
			}

		}
		
	}
}
