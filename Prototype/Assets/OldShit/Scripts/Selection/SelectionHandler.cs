using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{ // selection and actions

	public delegate void UIPanelOperation(Unit unit);
	public static event UIPanelOperation OnUnitUnselected;
    public static event UIPanelOperation OnUnitSelected;

    [SerializeField] private Camera camera;
    [SerializeField] private MouseInput mouseInput;

    HashSet<WorldObject> allUnits;
    HashSet<WorldObject> objectsInsideFrustum;
    HashSet<WorldObject> selectedUnits;
    WorldObject currentlyHighlightedObject; // onMouseHover

    [SerializeField] private PerkHandler perks;
    [SerializeField] private SkillsPanelManager skillsPanelManager;
    private CitizenUpgradeHandler citizenUpgradeHandler;

    private bool isNeutralObjectSelected;
    private bool isShiftDown = false;

    public bool IsShiftDown { get { return isShiftDown; } set { isShiftDown = value; } }

    public HashSet<WorldObject> ObjectsInsideFrustum { get { return objectsInsideFrustum; } }
    public HashSet<WorldObject> SelectedUnits { get { return selectedUnits; } }
    public HashSet<WorldObject> AllPlayerUnits { get { return allUnits; } }
    public PerkHandler Perks { get { return perks; } }
    public CitizenUpgradeHandler CitizenUpgradeHandler { get { return citizenUpgradeHandler; } }
    public MouseInput MouseInput { get { return mouseInput; } }


	void Awake()
	{
		objectsInsideFrustum = new HashSet<WorldObject> ();
		selectedUnits = new HashSet<WorldObject> ();
        allUnits = new HashSet<WorldObject>();
        perks = new PerkHandler (this, skillsPanelManager);
        citizenUpgradeHandler = new CitizenUpgradeHandler(this);

	}

	public void OnMouseHover (Vector3 mousePosition)
	{
		var ray = camera.ScreenPointToRay (mousePosition);

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();

			if (worldObject != null && !selectedUnits.Contains(worldObject)) { // НАДО ЧТО-ТО ДЕЛАТЬ С КОДОМ В ЭТОМ IF!!11

				if (!worldObject.IsVisibleInGame)
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
        WorldObject currentlySelected = null;

		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			var worldObject = hit.collider.gameObject.GetComponent<WorldObject> ();
            if (worldObject != null && worldObject.IsVisibleInGame) {
                if (!worldObject.IsSelected)
                    SelectObject(worldObject);
                currentlySelected = worldObject;
			}
		}

        if (!isShiftDown)
            removeSelection(currentlySelected);

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
					SelectObject (worldObject);
				}
			} else {
				if (worldObject.IsSelected) {
					UnselectObject (worldObject);
				}
			}
		}
	}

    public void SelectObject(WorldObject worldObject)
	{
		worldObject.IsSelected = true;
		worldObject.Highlight ();
		selectedUnits.Add(worldObject);

		if (worldObject is Unit) {
            if (OnUnitSelected != null)
                OnUnitSelected(worldObject as Unit);
            //if(worldObject.Owner.IsHuman)
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
		
		if (worldObject is Unit) {
            if (OnUnitUnselected != null)
                OnUnitUnselected(worldObject as Unit);
            if (worldObject.Owner.IsHuman)
			    perks.RemovePerks (worldObject as Unit);
		}
	}


    private void removeSelection(WorldObject exception = null)
	{
		List<WorldObject> deletionList = new List<WorldObject>(selectedUnits);
		foreach (WorldObject worldObject in deletionList) {
            if(exception == null || !exception.Equals(worldObject))
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
