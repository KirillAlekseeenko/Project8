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
	[SerializeField] private Text buildingName;

	[SerializeField] private GameObject buildPanel;
	[SerializeField] private GameObject upgradesPanel;
	[SerializeField] private GameObject technologyPanel;
	[SerializeField] private GameObject thisTechInfoPanel;
	[SerializeField] private GameObject buildingLevelInfoPanel;
	[SerializeField] public Text infoPanel;

	public List<GameObject> clickedPanels;

	[HideInInspector] public Building currentBuilding;

	void Awake(){
		showPanel (false);

		clickedPanels = new List<GameObject> ();
		clickedPanels.Add (buildPanel);
		clickedPanels.Add (upgradesPanel);
		clickedPanels.Add (thisTechInfoPanel);
		clickedPanels.Add (buildingLevelInfoPanel);
		clickedPanels.Add (technologyPanel);
		clickedPanels.Add (infoPanel.gameObject);
	}
		
	void Update(){
		if (!checkClicking ()) {
			closeAllPanels ();
		}
	}

	private void closeAllPanels(){
		showUpgradesPanel (false);
		showPanel (false);
		showUnitsInside (false);
		showThisTechInfoPanel (false);
		showTechnologiesPanel (false);
		ShowLevelInfoPanel (false);
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
		if (enable) {
			buildingName.text = currentBuilding.BuildingName;
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
		}
	}

	public void showPanel(bool enable, Building building){
		buildPanel.SetActive (enable);
		currentBuilding = building;
		if (enable) {
			buildingName.text = currentBuilding.BuildingName;
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
		}
	}

	public void showUpgradesPanel(bool enable){
		upgradesPanel.SetActive (enable);
		if (enable) {
			showUnitsInside (false);
			showTechnologiesPanel (false);
			showThisTechInfoPanel (false);
			ShowLevelInfoPanel (false);
			
		}
	}

	public void showUnitsInside(bool enable){
		if (enable) {
			showUpgradesPanel (false);
			showTechnologiesPanel (false);
			showThisTechInfoPanel (false);
			ShowLevelInfoPanel (false);
		}
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

	public void showTechnologiesPanel(bool enable){
		if (enable) {
			showUnitsInside (false);
			showUpgradesPanel (false);
			showThisTechInfoPanel (false);
			ShowLevelInfoPanel (false);
		}
		technologyPanel.GetComponent<BuildingTechnology> ().PanelOpened (enable);
	}

	public void showThisTechInfoPanel(bool enable){
		thisTechInfoPanel.SetActive (enable);
	}

	public void ShowLevelInfoPanel(bool enable){
		buildingLevelInfoPanel.SetActive (enable);
	}

	#endregion

	public void UpgradeCurrentBuilding(int upgradeLevel){
		if (upgradeLevel == currentBuilding.CurrentLevel + 1) {
			currentBuilding.CurrentLevel = upgradeLevel;
			currentImage.sprite = currentBuilding.vTools.buildingLevels [currentBuilding.CurrentLevel - 1].image;
		}
	}

	public void removeAllUnitsFromBuilding(){
		currentBuilding.RemoveAllUnits ();
	}
}
