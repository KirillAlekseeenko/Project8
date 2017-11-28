using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
	private Unit unit;

	private SelectionHandler selectionHandler;

	public Unit Unit {
		get {
			return unit;
		}
		set {
			unit = value;
		}
	}

	public SelectionHandler SelectionHandler {
		get {
			return selectionHandler;
		}
		set {
			selectionHandler = value;
		}
	}

	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{
		
		selectionHandler.selectOneUnit (unit);
		selectionHandler.confirmSelection ();
	}
	#endregion

	#region IPointerEnterHandler implementation

	public void OnPointerEnter (PointerEventData eventData)
	{
		transform.localScale = new Vector3 (1, 1.4f, 1);
	}

	#endregion

	#region IPointerExitHandler implementation

	public void OnPointerExit (PointerEventData eventData)
	{
		transform.localScale = Vector3.one;
	}

	#endregion
}
