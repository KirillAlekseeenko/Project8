using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingPanelManager : MonoBehaviour {

	public delegate void UIPanelOperation(Unit unit);
	public static event UIPanelOperation OnUnitAdded;
	public static event UIPanelOperation OnUnitRemoved;

	[SerializeField] private Image currentImage;

	[SerializeField] private GameObject buildPanel;
	[SerializeField] private GameObject upgradesPanel;
	[SerializeField] private GameObject buildingInfo;
	[SerializeField] public Text infoPanel;

	private List<GameObject> clickedPanels;

	[HideInInspector] public Building currentBuilding;

	void Awake(){
		showPanel (false);

		clickedPanels = new List<GameObject> ();
		clickedPanels.Add (buildPanel);
		clickedPanels.Add (upgradesPanel);
		clickedPanels.Add (buildingInfo);
		clickedPanels.Add (infoPanel.gameObject);
	}
		
	void Update(){
		if (!checkClicking ()) {
			showUpgradesPanel (false);
			showPanel (false);
			showUnitsInside (false);
		}
	}

	private bool checkClicking(){
		if (Input.GetMouseButtonDown (0)) {
			foreach(GameObject obj in clickedPanels){
				if (RectTransformUtility.RectangleContainsScreenPoint (
					   obj.GetComponent<RectTransform> (), 
					   Input.mousePosition))
					return true;
			}	
			return false;
		}
		return true;
	}
	#region Show/hide subpanels
	public void showPanel(bool enable){
		buildPanel.SetActive (enable);
		if(enable)
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
	}

	public void showPanel(bool enable, Building building){
		buildPanel.SetActive (enable);
		currentBuilding = building;
		if(enable)
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
	}

	public void showUpgradesPanel(bool enable){
		upgradesPanel.SetActive (enable);
		buildingInfo.SetActive (enable);
		if (enable) {
			showUnitsInside (false);
			
		}
	}

	public void showUnitsInside(bool enable){
		if (enable)
			showUpgradesPanel (false);
		if (currentBuilding != null) {
			if (enable) {
				foreach (Unit unit in currentBuilding.UnitsInside)
					OnUnitAdded (unit);
			} else {
				try{
				foreach (Unit unit in currentBuilding.UnitsInside)
						OnUnitRemoved (unit);
				}catch(Exception){}
			}
		}
	}

	#endregion

	public void UpgradeCurrentBuilding(int upgradeLevel){
		if (upgradeLevel == currentBuilding.CurrentLevel + 1) {
			currentBuilding.CurrentLevel = upgradeLevel;
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
		}
	}
}
