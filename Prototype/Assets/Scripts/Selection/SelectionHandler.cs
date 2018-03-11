using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour { // selection and actions

	[System.Serializable]
	public class PerkInfo
	{
		[SerializeField] int perkCount = 1;
		[SerializeField] string name;
		[SerializeField] PerkType type;

		public PerkInfo(string name, Perk perk)
		{
			this.name = name;
			type = perk.Type;
		}

		public string Name {
			get {
				return name;
			}
		}

		public PerkType Type {
			get {
				return type;
			}
		}

		public int PerkCount {
			get {
				return perkCount;
			}
			set {
				perkCount = value;
			}
		}
	}




	[System.Serializable]
	public class PerkHandler
	{
		private SelectionHandler selectionHandler;
		[SerializeField] private List<PerkInfo> unitPerks;
		[SerializeField] private List<Unit> activatedUnits;
		private PerkInfo currentPerk;

		public PerkInfo CurrentPerk {
			get {
				return currentPerk;
			}
		}

		public PerkHandler(SelectionHandler selectionHandler)
		{
			unitPerks = new List<PerkInfo>();
			activatedUnits = new List<Unit>();
			this.selectionHandler = selectionHandler;
		}
		public void AddPerks(Unit unit)
		{
			foreach (var perk in unit.PerkList) {
				var perkInfo = unitPerks.Find (x => x.Name.Equals (perk.Name));
				if (perkInfo != null) {
					perkInfo.PerkCount++;
				} else {
					unitPerks.Add (new PerkInfo (perk.Name, perk));
				}
			}
		}
		public void RemovePerks(Unit unit)
		{
			foreach (var perk in unit.PerkList) {
				var perkInfo = unitPerks.Find (x => x.Name.Equals (perk.Name));
				if (perkInfo != null) {
					perkInfo.PerkCount--;
					if (perkInfo.PerkCount <= 0) {
						Debug.Log ("Remove");
						unitPerks.Remove (perkInfo);
					}
				}
			}
			if (activatedUnits.Contains (unit)) {
				activatedUnits.Remove (unit);
				if (activatedUnits.Count == 0)
					deactivate ();
			}
		}
		public void ActivatePerk(int index)
		{
			if (index >= unitPerks.Count) {
				Debug.Log (unitPerks);
				return;
			}
			deactivate ();
			PerkInfo perk = unitPerks [index];
			currentPerk = perk;
			Debug.Log (selectionHandler.SelectedUnits.Count);
			foreach (var worldObject in selectionHandler.SelectedUnits) {
				if (worldObject is Unit) {
					var unit = worldObject as Unit; 
					var perkToActivate = unit.PerkList.Find (x => x.Name.Equals(perk.Name));
					//Debug.Log (perkToActivate);
					if (perkToActivate != null) {
						if (perk.Type == PerkType.Itself) {
							perkToActivate.Run (unit);
						} else {
							activatedUnits.Add (unit);
						}
					}
				}
			}

			if (!(perk.Type == PerkType.Itself)) {
				selectionHandler.mouseInput.PerkModeOn ();
			}
		}
		private void performPerk(Vector3? place = null, Unit target = null)
		{
			if (activatedUnits.Count > 0) {
				int index = 0;
				while (index < activatedUnits.Count && !activatedUnits [index].PerkList.Find (x => x.Name.Equals (currentPerk.Name)).isReadyToFire) { // first reloaded
					index++;
				}
				var unit = activatedUnits [index]; 
				var perkToActivate = unit.PerkList.Find (x => x.Name.Equals (currentPerk.Name));
				if (perkToActivate != null) {
					if (target == null && currentPerk.Type == PerkType.Target)
						return;
					if (currentPerk.Type == PerkType.Ground) {
						perkToActivate.Run (unit, place: place);
					} else if (currentPerk.Type == PerkType.Target) {
						perkToActivate.Run (unit, target: target);
					}
				}
			}
			deactivate ();
		}
		public void OnLeftButtonDown(Vector3 mousePosition)
		{
			var ray = Camera.main.ScreenPointToRay (mousePosition);

			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				var unit = hit.collider.gameObject.GetComponent<Unit> ();
				if (unit != null && unit.IsVisible && Player.HumanPlayer.isEnemy(unit.Owner)) {
					performPerk (unit.transform.position, unit);
				} else {
					performPerk (hit.point);
				}
			}
		}
		public void OnRightButtonDown(Vector3 mousePosition)
		{
			deactivate ();
		}

		private void deactivate()
		{
			activatedUnits.Clear ();
			currentPerk = null;
			selectionHandler.mouseInput.PerkModeOff ();
		}
	}





	[SerializeField] private Camera camera;
	[SerializeField] private MouseInput mouseInput;

	HashSet<WorldObject> objectsInsideFrustum;
	HashSet<WorldObject> selectedUnits;
	WorldObject currentlyHighlightedObject; // onMouseHover

	[SerializeField] private PerkHandler perks;

	private bool isNeutralObjectSelected;
	private bool isShiftDown = false;

	public bool IsShiftDown {
		get {
			return isShiftDown;
		}
		set {
			isShiftDown = value;
		}
	}

	public HashSet<WorldObject> ObjectsInsideFrustum {
		get {
			return objectsInsideFrustum;
		}
	}

	public HashSet<WorldObject> SelectedUnits {
		get {
			return selectedUnits;
		}
	}

	public PerkHandler Perks {
		get {
			return perks;
		}
	}


	void Awake()
	{
		objectsInsideFrustum = new HashSet<WorldObject> ();
		selectedUnits = new HashSet<WorldObject> ();
		perks = new PerkHandler (this);
	}

	public void OnMouseHover (Vector3 mousePosition)
	{
		var ray = camera.ScreenPointToRay (mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();

			if (worldObject != null && !selectedUnits.Contains(worldObject)) { // НАДО ЧТО-ТО ДЕЛАТЬ С КОДОМ В ЭТОМ IF!!11

				if (!worldObject.IsVisible)
					return;

				if (currentlyHighlightedObject != null && !selectedUnits.Contains(currentlyHighlightedObject)) {
					currentlyHighlightedObject.Dehighlight ();
				}

				worldObject.Highlight ();
				currentlyHighlightedObject = worldObject;

			} else {
				if (currentlyHighlightedObject != null) {
					
					if(!selectedUnits.Contains(currentlyHighlightedObject))
						currentlyHighlightedObject.Dehighlight ();
					
					currentlyHighlightedObject = null;
				}
			}
		} 
	}
	public void OnLeftButtonDown (Vector3 mousePosition)
	{
		var ray = camera.ScreenPointToRay (mousePosition);

		if (!isShiftDown)
			removeSelection ();

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();
			if (worldObject != null && worldObject.IsVisible) {
				selectObject (worldObject);
			}
		}

	}
	public void OnLeftButtonUp (Vector3 mousePosition)
	{
		//Debug.Log (selectedUnits.Count);
	}
	public void OnDrag(Vector3 clickPosition, Vector3 mousePosition)
	{
		foreach (WorldObject worldObject in objectsInsideFrustum) {
			
			if (IsWithinSelectionBounds (worldObject, clickPosition, mousePosition) && worldObject.Owner.IsHuman && worldObject is Unit) {
				if (!worldObject.IsSelected) {
					selectObject (worldObject);
				}
			} else {
				if (worldObject.IsSelected) {
					UnselectObject (worldObject);
				}
			}
		}
	}

	private void selectObject(WorldObject worldObject)
	{
		worldObject.IsSelected = true;
		worldObject.Highlight ();
		selectedUnits.Add(worldObject);

		if (worldObject is Unit && worldObject.Owner.IsHuman) {
			perks.AddPerks (worldObject as Unit);
		}
	}
	public void UnselectObject(WorldObject worldObject)
	{
		if (!selectedUnits.Contains (worldObject))
			return;
		
		worldObject.IsSelected = false;
		worldObject.Dehighlight ();
		selectedUnits.Remove(worldObject);

		if (worldObject is Unit && worldObject.Owner.IsHuman) {
			perks.RemovePerks (worldObject as Unit);
		}
	}


	private void removeSelection()
	{
		List<WorldObject> deletionList = new List<WorldObject>(selectedUnits);
		foreach (WorldObject worldObject in deletionList) {
			UnselectObject (worldObject);
		}
	}

	private bool IsWithinSelectionBounds( WorldObject worldObject, Vector3 mousePos1, Vector3 mousePos2 )
	{
		var camera = Camera.main;
		var viewportBounds = UIUtils.GetViewportBounds( camera, mousePos1, mousePos2 );

		return viewportBounds.Contains ( camera.WorldToViewportPoint( worldObject.transform.position ) );
	}

}
